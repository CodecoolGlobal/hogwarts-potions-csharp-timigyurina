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
    }
}
