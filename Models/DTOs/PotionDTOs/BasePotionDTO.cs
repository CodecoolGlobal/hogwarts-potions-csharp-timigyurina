using HogwartsPotions.Models.DTOs.IngredientDTOs;
using HogwartsPotions.Models.DTOs.RecipeDTOs;
using HogwartsPotions.Models.DTOs.StudentDTOs;
using System.ComponentModel.DataAnnotations;

namespace HogwartsPotions.Models.DTOs.PotionDTOs
{
    public abstract class BasePotionDTO
    {
        [Required]
        public int StudentId { get; set; }
    }

    public class AddPotionDTO : BasePotionDTO
    {
        public HashSet<IngredientDTOWithId> Ingredients { get; set; }
    }

    public class GetPotionDTO : BasePotionDTO  // for GetAll
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BrewingStatus { get; set; }

        public int? RecipeId { get; set; }
    }

    public class GetPotionDTOWithDetails : BasePotionDTO  // for Startbrewing
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BrewingStatus { get; set; }

        public StudentDTOWithId Student { get; set; }


        public RecipeDTOWithId? Recipe { get; set; }

        public HashSet<IngredientDTOWithId> Ingredients { get; set; }// = new HashSet<IngredientDTOWithId>();
    }

    public class GetPotionDTOWithRecipeAndPotionIngredientDetails : BasePotionDTO // for Get with details
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BrewingStatus { get; set; }

        public StudentDTOWithId Student { get; set; }

        public GetRecipeDTOWithDetails? Recipe { get; set; }
        public HashSet<PotionIngredientDTO>? PotionIngredients { get; set; }
    }
}
