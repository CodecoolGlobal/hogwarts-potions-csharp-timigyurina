using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Repositories.Interfaces
{
    public interface IRecipeRepository : IGenericRepository<Recipe>
    {
        Task<Recipe?> CheckIfRecipeExistsWithIngredients(IEnumerable<Ingredient> ingredients);
        Task<Recipe?> CreateNewAsync(Student creator);
        Task<Recipe?> GetWithDetails(int? id);
        Task<HashSet<Ingredient>?> GetIngredientsOfRecipe(int? recipeId);
        IEnumerable<Recipe> GetRecipesWithIngredients(IEnumerable<Ingredient> ingredients);
        Task<IEnumerable<Recipe>> GetStudentRecipes(int studentId);
    }
}
