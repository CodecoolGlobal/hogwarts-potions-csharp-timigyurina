using HogwartsPotions.Models.DTOs.IngredientDTOs;
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

    public class AddRecipeDTO : BaseRecipeDTO
    {
        public HashSet<IngredientDTOWithId> Ingredients { get; set; }
    }

    public class GetRecipeDTOWithDetails : BaseRecipeDTO  // for GetHalp method in PotionsController
    {
        public int Id { get; set; }
        public StudentDTOWithId Student { get; set; }



        public ICollection<ConsistencyDTO> Consistencies { get; set; }
        public HashSet<IngredientDTOWithId> Ingredients { get; set; }

    }

    public class RecipeDTOWithId : BaseRecipeDTO  // for getting Potions and Ingredients (n RecipeDTOWithId)
    {
        public int Id { get; set; }
    }
}
