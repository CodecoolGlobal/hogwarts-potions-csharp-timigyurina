using HogwartsPotions.Data;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Repositories.Implementations
{
    public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(HogwartsContext context) : base(context)
        {
        }

        public Recipe? CheckIfRecipeExistsWithIngredients(IEnumerable<Ingredient> ingredients)
        {
            IEnumerable<Recipe> detailedRecipesWithIngredientCount = _context.Recipes.Where(r => r.Consistencies.Count() == ingredients.Count())
                .Include(r => r.Consistencies)
                .AsNoTracking();

            IEnumerable<int> idsOfIngredientsToCheck = ingredients.Select(i => i.Id);

            foreach (Recipe recipe in detailedRecipesWithIngredientCount)
            {
                IEnumerable<int> ingredientIdsInRecipe = recipe.Consistencies.Select(c => c.IngredientId);

                bool areAllIngredientsInRecipe = idsOfIngredientsToCheck.Intersect(ingredientIdsInRecipe).Count() == idsOfIngredientsToCheck.Count();

                if (areAllIngredientsInRecipe)
                {

                    return recipe;
                }

            }
            return null;
        }

        public async Task<Recipe?> CreateNewAsync(Student creator)
        {
            Recipe recipeToAdd = new Recipe()
            {
                Name = $"Student#{creator.Id}'s recipe",
                StudentId = creator.Id
            };

            return await AddAsync(recipeToAdd);
        }

        public async Task<HashSet<Ingredient>?> GetIngredientsOfRecipe(int? recipeId)
        {
            if (recipeId == null)
            {
                return null;
            }
            Recipe? recipe = await GetWithDetails(recipeId);
            if (recipe == null)
            {
                return null;
            }

            HashSet<Ingredient> ingredients = new HashSet<Ingredient>();
            foreach (Consistency consistency in recipe.Consistencies)
            {
                ingredients.Add(consistency.Ingredient);
            }

            return ingredients;
        }


        public async Task<Recipe?> GetWithDetails(int? id)
        {
            return await _context.Recipes
                .Include(r => r.Consistencies)
                    .ThenInclude(c => c.Ingredient)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public IEnumerable<Recipe> GetRecipesWithIngredients(IEnumerable<Ingredient> ingredients)
        {
            HashSet<Recipe> recipesWithIngredients = new HashSet<Recipe>();

            IEnumerable<int> idsOfIngredientsToCheck = ingredients.Select(i => i.Id);

            IEnumerable<Recipe> detailedRecipesWithIngredientCount = _dbSet.Where(r => r.Consistencies.Count() >= ingredients.Count())
                .Include(r => r.Consistencies)
                    .ThenInclude(c => c.Ingredient)
                .AsNoTracking();

            foreach (Recipe recipe in detailedRecipesWithIngredientCount)
            {
                IEnumerable<int> ingredientIdsInRecipe = recipe.Consistencies.Select(c => c.IngredientId);

                bool containsAllIngredients = idsOfIngredientsToCheck.Intersect(ingredientIdsInRecipe).Count() >= idsOfIngredientsToCheck.Count();

                if (containsAllIngredients)
                {
                    recipesWithIngredients.Add(recipe);
                }

            }
            return recipesWithIngredients;
        }
    }
}
