using HogwartsPotions.Data;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using HogwartsPotions.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Repositories.Implementations
{
    public class PotionRepository : GenericRepository<Potion>, IPotionRepository
    {
        public PotionRepository(HogwartsContext context) : base(context)
        {

        }

        public async Task<Potion?> CreateNewAsync(Student creator, BrewingStatus brewingStatus, Recipe? recipe)
        {

            Potion potionToBeAdded = new Potion()
            {
                StudentId = creator.Id,
                RecipeId = recipe == null ? null : recipe.Id,
                BrewingStatus = brewingStatus,
                Name = $"Student#{creator.Id}'s {brewingStatus} {(recipe == null ? "" : $"of Recipe#{recipe.Id}")}"
            };

            return await AddAsync(potionToBeAdded);
        }

        public async Task<IEnumerable<Potion>> GetStudentPotions(int studentId)
        {
            return await GetAllAsync(p => p.StudentId == studentId);
        }

        public Task<Potion?> GetPotionWithDetails(int id)
        {
            return _context.Potions
                .Include(p => p.Student)
                .Include(p => p.Recipe)
                .Include(p => p.PotionIngredients)
                    .ThenInclude(pi => pi.Ingredient)
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public Task<Potion?> GetPotionWithPotionIngredients(int id)
        {
            return _context.Potions
                .Include(p => p.PotionIngredients)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Potion?> StartBrewing(Student creator)
        {
            Potion startedPotion = new Potion()
            {
                StudentId = creator.Id,
                BrewingStatus = BrewingStatus.Brew,
                Name = $"Student#{creator.Id}'s freshly started {BrewingStatus.Brew}",
                RecipeId = null,
            };

            return await AddAsync(startedPotion);
        }

        public async Task<Potion?> UpdateBasedOnAddedIngredient(int potionId, BrewingStatus brewingStatus, Recipe? recipe)
        {
            Potion? potionToBeUpdated = await _dbSet
                .FirstOrDefaultAsync(p => p.Id == potionId);

            if (potionToBeUpdated == null)
                return null;

            if (potionToBeUpdated.PotionIngredients.Count > 4)
            {
                potionToBeUpdated.BrewingStatus = brewingStatus;
                if (recipe != null)
                {
                    potionToBeUpdated.RecipeId = recipe.Id;
                    potionToBeUpdated.Recipe = recipe;
                    potionToBeUpdated.Name = $"Student#{potionToBeUpdated.StudentId}'s {brewingStatus} of Recipe#{recipe.Id}";
                }
                await UpdateAsync(potionToBeUpdated);
            }
            return potionToBeUpdated;

        }

        public async Task<HashSet<Potion>> GetPotionsOfRecipe(int recipeId)
        {
            IEnumerable<Potion> potionsOfRecipe = await GetAllAsync(r => r.RecipeId == recipeId);
            return potionsOfRecipe.ToHashSet();
        }
    }
}
