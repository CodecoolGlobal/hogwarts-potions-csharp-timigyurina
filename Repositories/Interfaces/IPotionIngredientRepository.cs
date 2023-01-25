using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Repositories.Interfaces
{
    public interface IPotionIngredientRepository : IGenericRepository<PotionIngredient> 
    { 
        Task<bool> AddMoreForNewPotion(int potionId, HashSet<Ingredient> ingredientsOfPotion);
        bool CheckIfContains(int potionId, int ingredientId);
    }
}
