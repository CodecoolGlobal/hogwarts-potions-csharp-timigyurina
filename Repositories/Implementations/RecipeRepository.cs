﻿using HogwartsPotions.Data;
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

        public async Task<Recipe?> CheckIfRecipeExistsWithIngredients(IEnumerable<Ingredient> ingredients)
        {
            IEnumerable<Recipe> detailedRecipesWithIngredientCount = _context.Recipes.Where(r => r.Consistencies.Count() == ingredients.Count())
                .Include(r => r.Consistencies)
                .AsNoTracking();

            IEnumerable<int> idsOfIngredientsToCheck = ingredients.Select(i => i.Id);
            //IEnumerable<Recipe> detailedRecipes = _context.Recipes.Include(r => r.Consistencies).AsNoTracking();

            foreach (Recipe recipe in detailedRecipesWithIngredientCount)
            {
                IEnumerable<int> ingredientIdsInRecipe = recipe.Consistencies.Select(c => c.IngredientId);

                bool areAllIngredientsInRecipe = idsOfIngredientsToCheck.Intersect(ingredientIdsInRecipe).Count() == idsOfIngredientsToCheck.Count();

                if (areAllIngredientsInRecipe)
                {
                    Recipe? r = await GetAsync(recipe.Id);
                    return r;
                }

            }
            return null;
        }

        public async Task<Recipe?> CreateNewAsync(Student creator)
        {
            Recipe recipeToAdd = new Recipe()
            {
                Name = $"{creator.Name}'s recipe",
                StudentId = creator.Id
            };

            return await AddAsync(recipeToAdd);
        }
    }
}
