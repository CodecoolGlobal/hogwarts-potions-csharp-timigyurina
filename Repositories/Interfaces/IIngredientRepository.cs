using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Repositories.Interfaces
{
    public interface IIngredientRepository : IGenericRepository<Ingredient>
    {
        Ingredient? GetIngredientByName(string name);
        Task<HashSet<Ingredient>> GetIngredientsOfPotion(Potion potion);
    }
}
