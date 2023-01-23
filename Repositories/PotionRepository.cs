using HogwartsPotions.Data;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Repositories
{
    public class PotionRepository : GenericRepository<Potion>, IPotionRepository
    {
        public PotionRepository(HogwartsContext context) : base(context)
        {
        }
    }
}
