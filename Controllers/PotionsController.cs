using AutoMapper;
using HogwartsPotions.Data;
using HogwartsPotions.Models.DTOs.IngredientDTOs;
using HogwartsPotions.Models.DTOs.PotionDTOs;
using HogwartsPotions.Models.DTOs.RecipeDTOs;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HogwartsPotions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PotionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PotionsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        // GET: api/Potions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPotionDTO>>> GetAllPotions()
        {
            IEnumerable<Potion> potions = await _unitOfWork.PotionRepository.GetAllAsync();
            IEnumerable<GetPotionDTO> potionDTOs = _mapper.Map<IEnumerable<GetPotionDTO>>(potions);
            return Ok(potionDTOs);
        }


        // GET: api/Potions/student/1
        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<IEnumerable<GetPotionDTO>>> GetStudentPotions(int studentId)
        {
            Student? student = await _unitOfWork.StudentRepository.GetAsync(studentId);
            if (student == null)
                return BadRequest(JsonConvert.SerializeObject(new { message = $"There is no student with the id of {studentId}" }));

            IEnumerable<Potion> potions = await _unitOfWork.PotionRepository.GetStudentPotions(studentId);
            IEnumerable<GetPotionDTO> potionDTOs = _mapper.Map<IEnumerable<GetPotionDTO>>(potions);
            return Ok(potionDTOs);
        }

        // GET: api/Potions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPotionDTO>> GetPotionById(int id)
        {
            Potion? potion = await _unitOfWork.PotionRepository.GetAsync(id);

            if (potion == null)
                return NotFound(JsonConvert.SerializeObject(new { message = $"No Potion with the id of {id} was found" }));

            GetPotionDTO potionDTO = _mapper.Map<GetPotionDTO>(potion);

            return Ok(potionDTO);
        }

        // GET: api/Potions/details/5
        [HttpGet("details/{id}")]
        public async Task<ActionResult<GetPotionDTOWithRecipeAndPotionIngredientDetails>> GetPotionWithDetails(int id)
        {
            Potion? potion = await _unitOfWork.PotionRepository.GetPotionWithDetails(id);

            if (potion == null)
                return NotFound(JsonConvert.SerializeObject(new { message = $"No Potion with the id of {id} was found" }));

            // could also use IngredientRepository.GetIngredientsOfPotion method (but with searching for the Potion with the GetPotionWithDetails method, the Ingredients will also be included! - thanks to PotionIngredients joint table:))
            GetPotionDTOWithRecipeAndPotionIngredientDetails potionDTO = _mapper.Map<GetPotionDTOWithRecipeAndPotionIngredientDetails>(potion);

            return Ok(potionDTO);
        }

        // POST: api/Potions
        [HttpPost]
        public async Task<ActionResult<GetPotionDTO>> AddPotion(AddPotionDTO addPotionDTO)
        {
            Student? creator = await _unitOfWork.StudentRepository.GetAsync(addPotionDTO.StudentId);
            if (creator == null)
                return NotFound(JsonConvert.SerializeObject(new { message = $"Student with the id of {addPotionDTO.StudentId} does not exist" }));

            HashSet<Ingredient> ingredientsOfPotion = _mapper.Map<HashSet<Ingredient>>(addPotionDTO.Ingredients);
            BrewingStatus statusOfPotionToBeCreated = BrewingStatus.Brew;
            Recipe? recipeOfPotionToBeCreated = null;  // a Recipe will be assigned if the Potion has at least 5 ingredients (it was either found or created) 
            // Only check for Recipe if the Potion contains at least 5 ingredients (implicitly, a Recipe must contain at least 5 ingredients)
            if (addPotionDTO.Ingredients.Count > 4)
            {
                (Recipe, bool) recipeForPotionAndExistency;
                try
                {
                    recipeForPotionAndExistency = await CheckRecipeAndCreateIfNotExists(addPotionDTO.StudentId, ingredientsOfPotion); // returns either (existingRecipe,true) or (createdRecipe,false)
                    statusOfPotionToBeCreated = recipeForPotionAndExistency.Item2 ? BrewingStatus.Replica : BrewingStatus.Discovery; // true if the Recipe already existed
                    recipeOfPotionToBeCreated = recipeForPotionAndExistency.Item1;
                }
                catch (Exception exc)
                {
                    return BadRequest(exc.Message);
                }
            }

            Potion? createdPotion = await _unitOfWork.PotionRepository.CreateNewAsync(creator, statusOfPotionToBeCreated, recipeOfPotionToBeCreated);

            if (createdPotion == null)
                return BadRequest(JsonConvert.SerializeObject(new { message = $"There were some errors during the creation of the Potion in {nameof(AddPotion)}, Potion could not be created" }));

            // only add new PotionIngredients if Potion has been successfully created
            bool successfullyCreatedPotionIngredients = await _unitOfWork.PotionIngredientRepository.AddMoreForNewPotion(createdPotion.Id, ingredientsOfPotion);

            if (!successfullyCreatedPotionIngredients)
                return BadRequest(JsonConvert.SerializeObject(new { message = $"Errors during the creation of the PotionIngredients for the Potion in {nameof(AddPotion)}" }));

            GetPotionDTO createdPotionDTO = _mapper.Map<GetPotionDTO>(createdPotion);

            return CreatedAtAction("GetPotionById", new { id = createdPotion.Id }, createdPotionDTO);
        }

        // POST: api/Potions/brew/2
        [HttpPost("brew/{studentId}")]
        public async Task<ActionResult<GetPotionDTOWithDetails>> StartBrewing(int studentId)
        {
            Student? creator = await _unitOfWork.StudentRepository.GetAsync(studentId);
            if (creator == null)
                return BadRequest(JsonConvert.SerializeObject(new { message = $"Student with the id of {studentId} does not exist" }));

            Potion? startedPotion = await _unitOfWork.PotionRepository.StartBrewing(creator);
            if (startedPotion == null)
                return BadRequest(JsonConvert.SerializeObject(new { message = $"There were some errors during the brew of the Potion in {nameof(StartBrewing)}, Potion could not be created" }));

            GetPotionDTOWithDetails startedPotionDTO = _mapper.Map<GetPotionDTOWithDetails>(startedPotion);

            return CreatedAtAction("GetPotionById", new { id = startedPotion.Id }, startedPotionDTO);
        }

        // PUT: api/Potions/10/addIngredient
        [HttpPut("{potionId}/addIngredient")]
        public async Task<ActionResult<GetPotionDTOWithDetails>> AddIngredient(int potionId, IngredientDTO ingredientDTO)
        {
            Potion? potionToBeUpdated = await _unitOfWork.PotionRepository.GetAsync(potionId);
            if (potionToBeUpdated == null)
                return NotFound(JsonConvert.SerializeObject(new { message = $"No potion with the id of {potionId} was found" }));

            Ingredient? ingredientToBeAdded = _unitOfWork.IngredientRepository.GetIngredientByName(ingredientDTO.Name);
            if (ingredientToBeAdded == null)
            {
                ingredientToBeAdded = await _unitOfWork.IngredientRepository.AddAsync(new Ingredient() { Name = ingredientDTO.Name.ToLower() });
                if (ingredientToBeAdded == null)
                    return BadRequest(JsonConvert.SerializeObject(new { message = $"Ingredient could not be created." }));
            }

            bool alreadyContainsIngredient = _unitOfWork.PotionIngredientRepository.CheckIfContains(potionId, ingredientToBeAdded.Id);
            if (alreadyContainsIngredient)
                return BadRequest(JsonConvert.SerializeObject(new { message = $"Potion already contains this Ingredient" }));

            PotionIngredient? addedPotionIngredient = await _unitOfWork.PotionIngredientRepository.AddAsync(
                new PotionIngredient() { IngredientId = ingredientToBeAdded.Id, PotionId = potionToBeUpdated.Id }
            );

            Potion? potionToBeUpdatedWithPotionIngredients = await _unitOfWork.PotionRepository.GetPotionWithPotionIngredients(potionId);
            BrewingStatus statusAfterAddingIngredient = BrewingStatus.Brew;
            Recipe? recipeAfterAddingIngredient = null;  // a Recipe will be assigned if the Potion has at least 5 ingredients (it was either found or created) 

            // Only check for Recipe if the Potion contains at least 5 ingredients (implicitly, a Recipe must contain at least 5 ingredients)
            if (potionToBeUpdatedWithPotionIngredients != null && potionToBeUpdatedWithPotionIngredients.PotionIngredients.Count > 4)
            {
                HashSet<Ingredient> ingredientsOfPotionToBeUpdated = await _unitOfWork.IngredientRepository.GetIngredientsOfPotion(potionToBeUpdatedWithPotionIngredients);

                (Recipe, bool) recipeForPotionAndExistency;
                try
                {
                    recipeForPotionAndExistency = await CheckRecipeAndCreateIfNotExists(potionToBeUpdated.StudentId, ingredientsOfPotionToBeUpdated);
                    statusAfterAddingIngredient = recipeForPotionAndExistency.Item2 ? BrewingStatus.Replica : BrewingStatus.Discovery; // true if the Recipe already existed
                    recipeAfterAddingIngredient = recipeForPotionAndExistency.Item1;
                }
                catch (Exception exc)
                {
                    return BadRequest(exc.Message);
                }
            }

            Potion? updatedPotion = await _unitOfWork.PotionRepository.UpdateBasedOnAddedIngredient(potionId, statusAfterAddingIngredient, recipeAfterAddingIngredient);
            if (updatedPotion == null)
                return BadRequest(JsonConvert.SerializeObject(new { message = ($"There were some errors during the update of the Potion in {nameof(AddPotion)}, Potion could not be updated") }));

            Potion? updatedPotionWithMoreDetails = await _unitOfWork.PotionRepository.GetPotionWithDetails(potionId); // just to be able to view everything in the response
            GetPotionDTOWithRecipeAndPotionIngredientDetails updatedPotionDTO = _mapper.Map<GetPotionDTOWithRecipeAndPotionIngredientDetails>(updatedPotionWithMoreDetails);

            // If the Potion has a Recipe, get the number of Potions that have been made of this Recipe (for the frontend)
            if (updatedPotion.Recipe != null)
            {
                HashSet<Potion> potionsOfRecipe = await _unitOfWork.PotionRepository.GetPotionsOfRecipe(updatedPotion.Recipe.Id);
                HashSet<GetPotionDTO> potionDTOsOfRecipe = _mapper.Map<HashSet<GetPotionDTO>>(potionsOfRecipe);
                updatedPotionDTO.Recipe!.PotionsMadeOfRecipe = potionDTOsOfRecipe;
            }

            return Ok(updatedPotionDTO);
        }


        // GET: api/Potions/10/help
        [HttpGet("{potionId}/help")]
        public async Task<ActionResult<IEnumerable<GetRecipeDTOWithDetails>>> GetHelp(int potionId)
        {
            Potion? potion = await _unitOfWork.PotionRepository.GetPotionWithDetails(potionId);
            if (potion == null)
                return NotFound(JsonConvert.SerializeObject(new { message = $"No potion with the id of {potionId} was found" }));
            //if (potion.PotionIngredients.Count > 4)
            //    return BadRequest("The Potion already contains at least 5 Ingredients");

            IEnumerable<Ingredient> ingredientsOfPotion = potion.PotionIngredients.Select(p => p.Ingredient); // see comment in GetPotionWithDetails above
            IEnumerable<Recipe> recipesWithIngredients = _unitOfWork.RecipeRepository.GetRecipesWithIngredients(ingredientsOfPotion);

            IEnumerable<GetRecipeDTOWithDetails> recipeDTOs = _mapper.Map<IEnumerable<GetRecipeDTOWithDetails>>(recipesWithIngredients);
            return Ok(recipeDTOs);
        }


        // DELETE: api/Potions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePotionById(int id)
        {
            Potion? potion = await _unitOfWork.PotionRepository.GetAsync(id);

            if (potion == null)
                return NotFound(JsonConvert.SerializeObject(new { message = $"No Potion with the id of {id} exists" }));

            await _unitOfWork.PotionRepository.DeleteAsync(id);

            return Ok(JsonConvert.SerializeObject(new { message = $"Potion {id} was successfully deleted" }));
        }


        private async Task<Recipe> CreateRecipeAndConsistencies(int creatorId, HashSet<Ingredient> ingredients)
        {
            Student? creator = await _unitOfWork.StudentRepository.GetAsync(creatorId);
            if (creator == null)
                throw new Exception($"Student with the id of {creatorId} does not exist");

            Recipe? createdRecipe = await _unitOfWork.RecipeRepository.CreateNewAsync(creator);

            if (createdRecipe == null)
                throw new Exception($"Errors during the creation of the Recipe for the Potion in {nameof(CreateRecipeAndConsistencies)}, Potion could not be created");

            // only add new Consistencies if Recipe did not exist before
            bool successfullyCreatedConsistencies = await _unitOfWork.ConsistencyRepository.AddMoreForNewRecipe(createdRecipe.Id, ingredients);

            if (!successfullyCreatedConsistencies)
                throw new Exception($"Errors during the creation of the Consistencies for the Potion in {nameof(CreateRecipeAndConsistencies)}, Potion could not be created");

            return createdRecipe;
        }

        private async Task<(Recipe, bool)> CheckRecipeAndCreateIfNotExists(int creatorId, HashSet<Ingredient> ingredients)
        {
            Recipe? existingRecipe = await _unitOfWork.RecipeRepository.CheckIfRecipeExistsWithIngredients(ingredients);
            if (existingRecipe != null)
                return (existingRecipe, true);

            try
            {
                Recipe createdRecipe = await CreateRecipeAndConsistencies(creatorId, ingredients);
                return (createdRecipe, false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
