using AutoMapper;
using HogwartsPotions.Data;
using HogwartsPotions.Models.DTOs.IngredientDTOs;
using HogwartsPotions.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public IngredientsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientDTOWithId>>> GetAllIngredients()
        {
            IEnumerable<Ingredient> ingredients = await _unitOfWork.IngredientRepository.GetAllAsync();
            IEnumerable<IngredientDTOWithId> ingredientDTOs = _mapper.Map<IEnumerable<IngredientDTOWithId>>(ingredients);
            return Ok(ingredientDTOs);
        }


        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientDTOWithId>> GetIngredientById(int id)
        {
            Ingredient? ingredient = await _unitOfWork.IngredientRepository.GetAsync(id);

            if (ingredient == null)
                return NotFound("Ingredient was not found");

            IngredientDTOWithId ingredientDTO = _mapper.Map<IngredientDTOWithId>(ingredient);

            return Ok(ingredientDTO);
        }

    }
}
