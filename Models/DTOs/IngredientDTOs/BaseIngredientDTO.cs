using HogwartsPotions.Models.DTOs.RecipeDTOs;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Models.DTOs.IngredientDTOs
{
    public abstract class BaseIngredientDTO
    {
        public string Name { get; set; }

    }

    public class IngredientDTO : BaseIngredientDTO  // for Post
    {

    }
    
    public class GetIngredientDTO : BaseIngredientDTO  // for Get and GetAll
    {
        public int Id { get; set; }
        public HashSet<RecipeDTOWithId> Recipes { get; set; }
    }
    
    public class IngredientDTOWithId : BaseIngredientDTO  // for adding and getting Potions and Recipes
    {
        public int Id { get; set; }
    }
}
