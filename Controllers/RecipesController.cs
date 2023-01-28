using AutoMapper;
using HogwartsPotions.Data;
using HogwartsPotions.Models.DTOs.PotionDTOs;
using HogwartsPotions.Models.DTOs.RecipeDTOs;
using HogwartsPotions.Models.DTOs.RoomDTOs;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HogwartsPotions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RecipesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeDTOWithId>>> GetAllRecipes()
        {
            IEnumerable<Recipe> recipes = await _unitOfWork.RecipeRepository.GetAllAsync();
            IEnumerable<RecipeDTOWithId> recipeDTOs = _mapper.Map<IEnumerable<RecipeDTOWithId>>(recipes);
            return Ok(recipeDTOs);
        }


        // GET: api/Recipes/student/1
        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<IEnumerable<RecipeDTOWithId>>> GetStudentRecipes(int studentId)
        {
            Student? student = await _unitOfWork.StudentRepository.GetAsync(studentId);
            if (student == null)
                return BadRequest(JsonConvert.SerializeObject(new { message = $"There is no student with the id of {studentId}" }));

            IEnumerable<Recipe> recipes = await _unitOfWork.RecipeRepository.GetStudentRecipes(studentId);
            IEnumerable<RecipeDTOWithId> recipeDTOs = _mapper.Map<IEnumerable<RecipeDTOWithId>>(recipes);
            return Ok(recipeDTOs);
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDTOWithId>> GetRecipeById(int id)
        {
            Recipe? recipe = await _unitOfWork.RecipeRepository.GetAsync(id);

            if (recipe == null)
                return NotFound(JsonConvert.SerializeObject(new { message = $"No Recipe with the id of {id} was found" })); ;

            RecipeDTOWithId recipeDTO = _mapper.Map<RecipeDTOWithId>(recipe);

            return Ok(recipeDTO);
        }

        // GET: api/Recipes/details/5
        [HttpGet("details/{id}")]
        public async Task<ActionResult<GetRecipeDTOWithDetails>> GetRecipeWithDetails(int id)
        {
            Recipe? recipe = await _unitOfWork.RecipeRepository.GetWithDetails(id);

            if (recipe == null)
                return NotFound(JsonConvert.SerializeObject(new { message = $"No Recipe with the id of {id} was found" }));

            GetRecipeDTOWithDetails recipeDTO = _mapper.Map<GetRecipeDTOWithDetails>(recipe);

            HashSet<Potion> potionsOfRecipe = await _unitOfWork.PotionRepository.GetPotionsOfRecipe(id);
            HashSet<GetPotionDTO> potionDTOsOfRecipe = _mapper.Map<HashSet<GetPotionDTO>>(potionsOfRecipe);
            recipeDTO.PotionsMadeOfRecipe = potionDTOsOfRecipe;

            return Ok(recipeDTO);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNameOfRecipe(int id, UpdateRecipeDTO recipeDTO)
        {
            Recipe? recipe = await _unitOfWork.RecipeRepository.GetAsync(id);

            if (recipe == null)
                return NotFound(JsonConvert.SerializeObject(new { message = $"No Recipe with the id of {id} was found" }));

                _mapper.Map(recipeDTO, recipe);

            try
            {
                await _unitOfWork.RecipeRepository.UpdateAsync(recipe);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _unitOfWork.RecipeRepository.Exists(id))
                {
                    return NotFound(JsonConvert.SerializeObject(new { message = $"Something happened while trying to update the Recipe, please try again later." }));
                }
                else
                {
                    throw;
                }
            }

            RecipeDTOWithId updatedRecipeDTO = _mapper.Map<RecipeDTOWithId>(recipe);

            return Ok(updatedRecipeDTO);
        }


        // GET: api/Recipes/success
        [HttpGet("success")]
        public ActionResult<SuccessMessage> GetSuccessMessage()
        {
            SuccessMessage message = SuccessMessageGenerator.GetRandomMessage();

            return Ok(message);
        }
    }
}
