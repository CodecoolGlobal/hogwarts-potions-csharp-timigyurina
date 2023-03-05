using HogwartsPotions.Data;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Repositories.Interfaces;

namespace HogwartsPotions.Repositories.Implementations
{
    public class ConsistencyRepository : GenericRepository<Consistency>, IConsistencyRepository
    {
        public ConsistencyRepository(HogwartsContext context) : base(context)
        {
        }

        public async Task<bool> AddMoreForNewRecipe(int recipeId, HashSet<Ingredient> ingredientsOfRecipe)
        {
            List<Consistency> addedConsistencies = new List<Consistency>();
            foreach (Ingredient ingredient in ingredientsOfRecipe)
            {
                Consistency addedConsistency = await AddAsync(
                    new Consistency() { IngredientId = ingredient.Id, RecipeId = recipeId }
                );
                addedConsistencies.Add(addedConsistency);
            }

            return addedConsistencies.Count == ingredientsOfRecipe.Count;
        }
    }
}
