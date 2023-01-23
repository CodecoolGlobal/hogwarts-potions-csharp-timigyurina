using AutoMapper;
using HogwartsPotions.Data;
using HogwartsPotions.Models.DTOs.PotionDTOs;
using HogwartsPotions.Models.DTOs.StudentDTOs;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

        // GET: api/Potions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Potion>> GetPotionById(int id)
        {
            Potion? potion = await _unitOfWork.PotionRepository.GetAsync(id);

            if (potion == null)
            {
                return NotFound("Potion was not found");
            }

            GetPotionDTO potionDTO = _mapper.Map<GetPotionDTO>(potion);

            return Ok(potionDTO);
        }



        // POST: api/Potions
        [HttpPost]
        public async Task<ActionResult<Potion>> AddPotion(AddPotionDTO addPotionDTO)
        {
            Student? creator = await _unitOfWork.StudentRepository.GetAsync(addPotionDTO.StudentId);
            if (creator == null)
            {
                return BadRequest($"Student with the id of {addPotionDTO.StudentId} does not exist");
            }

            IEnumerable<Ingredient> ingredientsOfPotion = _mapper.Map<IEnumerable<Ingredient>>(addPotionDTO.Ingredients);

            Recipe? existingRecipe = await _unitOfWork.RecipeRepository.CheckIfRecipeExistsWithIngredients(ingredientsOfPotion);
            bool recipeExists = existingRecipe != null;

            Recipe? createdRecipe = new();
            if (!recipeExists)
            {
                createdRecipe = await _unitOfWork.RecipeRepository.CreateNewAsync(creator);

                if (createdRecipe == null)
                {
                    return BadRequest($"Errors during the creation of the Recipe for the Potion in {nameof(AddPotion)}, Potion could not be created");
                }
            }


            BrewingStatus brewingStatus = addPotionDTO.Ingredients.Count < 5 ? BrewingStatus.Brew : (recipeExists ? BrewingStatus.Replica : BrewingStatus.Discovery);

            Recipe recipeForPotion = recipeExists ? existingRecipe : createdRecipe;
            Potion? createdPotion = await _unitOfWork.PotionRepository.CreateNewAsync(creator, brewingStatus, recipeForPotion);

            if (createdPotion == null)
            {
                return BadRequest($"There were some errors during the creation of the Potion in {nameof(AddPotion)}, Potion could not be created");
            }

            GetPotionDTO createdPotionDTO = _mapper.Map<GetPotionDTO>(createdPotion);

            return CreatedAtAction("GetPotionById", new { id = createdPotion.Id }, createdPotionDTO);
        }


    }
}
