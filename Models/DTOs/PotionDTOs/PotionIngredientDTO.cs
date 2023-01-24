using HogwartsPotions.Models.DTOs.IngredientDTOs;

namespace HogwartsPotions.Models.DTOs.PotionDTOs
{
    public class PotionIngredientDTO
    {
        public int PotionId { get; set; }
        public int IngredientId { get; set; }
        //public Potion Potion { get; set; }
        public IngredientDTOWithId Ingredient { get; set; }
    }
}

