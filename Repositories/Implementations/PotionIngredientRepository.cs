using HogwartsPotions.Data;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Repositories.Interfaces;

namespace HogwartsPotions.Repositories.Implementations
{
    public class PotionIngredientRepository : GenericRepository<PotionIngredient>, IPotionIngredientRepository
    {
        public PotionIngredientRepository(HogwartsContext context) : base(context)
        {
        }

        public async Task<bool> AddMoreForNewPotion(int potionId, HashSet<Ingredient> ingredientsOfPotion)
        {
            List<PotionIngredient> addedPotionIngredients = new List<PotionIngredient>();
            foreach (Ingredient ingredient in ingredientsOfPotion)
            {
                PotionIngredient addedPotionIngredient = await AddAsync(
                    new PotionIngredient() { PotionId = potionId, IngredientId = ingredient.Id }
                );
                addedPotionIngredients.Add(addedPotionIngredient);
            }

            return addedPotionIngredients.Count == ingredientsOfPotion.Count;
        }
    }
}
