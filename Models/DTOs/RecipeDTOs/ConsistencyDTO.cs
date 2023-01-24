using HogwartsPotions.Models.DTOs.IngredientDTOs;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Models.DTOs.RecipeDTOs
{
    public class ConsistencyDTO
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public IngredientDTOWithId Ingredient { get; set; }
    }
}

