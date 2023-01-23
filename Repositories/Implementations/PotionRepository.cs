using HogwartsPotions.Data;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using HogwartsPotions.Repositories.Interfaces;

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
                BrewingStatus = brewingStatus,
                Name = $"{creator.Name}'s {brewingStatus} #{creator.Id}{recipe.Id}"
            };

            return await AddAsync(potionToBeAdded);
        }
    }
}
