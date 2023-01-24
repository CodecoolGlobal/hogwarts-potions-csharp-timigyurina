using HogwartsPotions.Data;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Repositories.Interfaces;

namespace HogwartsPotions.Repositories.Implementations
{
    public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(HogwartsContext context) : base(context)
        {
        }

        public Ingredient? GetIngredientByName(string name)
        {
            return _dbSet.FirstOrDefault(i => i.Name == name);
        }

        public async Task<HashSet<Ingredient>> GetIngredientsOfPotion(Potion potion)
        {
            HashSet<Ingredient> ingredients = new HashSet<Ingredient>();
            foreach (PotionIngredient potionIngredient in potion.PotionIngredients)
            {
                Ingredient? ingredient = await GetAsync(potionIngredient.IngredientId);
                if (ingredient != null)
                    ingredients.Add(ingredient);
            }

            return ingredients;
        }
    }
}
