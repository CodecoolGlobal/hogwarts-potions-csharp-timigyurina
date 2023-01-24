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

        public async Task<Potion?> CreateNewAsync(Student creator, BrewingStatus brewingStatus, Recipe recipe)
        {

            Potion potionToBeAdded = new Potion()
            {
                StudentId = creator.Id,
                RecipeId = recipe.Id,
                BrewingStatus = brewingStatus,
                Name = $"{creator.Name}'s {brewingStatus} #{creator.Id}{recipe.Id}"
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
                .Include(p => p.Recipe)
                    .ThenInclude(r => r.Consistencies)
                        .ThenInclude(c => c.Ingredient)
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Potion?> StartBrewing(Student creator)
        {
            Potion startedPotion = new Potion()
            {
                StudentId = creator.Id,
                BrewingStatus = BrewingStatus.Brew,
                Name = $"{creator.Name}'s freshly started {BrewingStatus.Brew}",
                RecipeId = null,
            };

            return await AddAsync(startedPotion);
        }

        public async Task<Potion?> UpdateBrewingStatusBasedOnIngredients(int potionId, bool hasRecipeExisted)
        {
            Potion?potionToBeUpdated = await _dbSet
                .Include(p => p.PotionIngredients)
                .FirstOrDefaultAsync(p => p.Id == potionId);

            if (potionToBeUpdated == null)
                return null;

            if (potionToBeUpdated.PotionIngredients.Count > 4)
            {
                potionToBeUpdated.BrewingStatus = hasRecipeExisted ? BrewingStatus.Replica : BrewingStatus.Discovery;
                await UpdateAsync(potionToBeUpdated);
            }
            return potionToBeUpdated;

        }
    }
}
