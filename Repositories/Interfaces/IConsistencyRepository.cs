using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Repositories.Interfaces
{
    public interface IConsistencyRepository : IGenericRepository<Consistency> 
    { 
        Task<bool> AddMoreForNewRecipe(int recipeId, HashSet<Ingredient> ingredientsOfRecipe);
    }
}
