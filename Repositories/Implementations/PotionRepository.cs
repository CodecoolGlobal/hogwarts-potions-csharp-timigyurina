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
    }
}
