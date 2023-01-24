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
            // return type: Task<IEnumerable<Consistency>>
            //List<Task<Consistency>> tasks = new();
            //foreach (Ingredient ingredient in ingredientsOfRecipe)
            //{
            //    tasks.Add(AddAsync(
            //        new Consistency() { IngredientId = ingredient.Id, RecipeId = recipeId }
            //    ));
            //}

            //IEnumerable<Consistency> addedConsistencies = await Task.WhenAll(tasks);
            //return addedConsistencies;

            // Solution above was not working, the error: https://learn.microsoft.com/en-gb/ef/core/dbcontext-configuration/#avoiding-dbcontext-threading-issues

            //Task t = Task.WhenAll(tasks);
            //bool allAdded = t.Status == TaskStatus.RanToCompletion;

            //return allAdded;

        }
    }
}
