using HogwartsPotions.Models.DTOs.IngredientDTOs;
using HogwartsPotions.Models.DTOs.PotionDTOs;
using HogwartsPotions.Models.DTOs.StudentDTOs;
using System.ComponentModel.DataAnnotations;

namespace HogwartsPotions.Models.DTOs.RecipeDTOs
{
    public abstract class BaseRecipeDTO
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; }


        [Required]
        public int StudentId { get; set; }
    }

    public class UpdateRecipeDTO 
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
    }

    public class GetRecipeDTOWithDetails : BaseRecipeDTO  // for GetHalp method in PotionsController, GetRecipeWithDetails in recipesController
    {
        public int Id { get; set; }
        public StudentDTOWithId Student { get; set; }
        public ICollection<ConsistencyDTO> Consistencies { get; set; }

        public HashSet<IngredientDTOWithId> Ingredients { get; set; }  // this prop is only for GetHalp method in PotionsController
        public HashSet<GetPotionDTO> PotionsMadeOfRecipe { get; set; }  // this prop is only for GetDetails method in RecipesController

    }

    public class RecipeDTOWithId : BaseRecipeDTO  // for getting Potions and Ingredients (n RecipeDTOWithId)
    {
        public int Id { get; set; }
    }
}
