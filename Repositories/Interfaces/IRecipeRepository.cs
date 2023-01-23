using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Repositories.Interfaces
{
    public interface IRecipeRepository : IGenericRepository<Recipe>
    {
       Task<Recipe?> CheckIfRecipeExistsWithIngredients(IEnumerable<Ingredient> ingredients);
       Task<Recipe?> CreateNewAsync(Student creator);
    }
}
