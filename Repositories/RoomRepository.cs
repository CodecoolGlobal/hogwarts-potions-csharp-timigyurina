using HogwartsPotions.Data;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Repositories
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(HogwartsContext context) : base(context)
        {
        }

        public Task<IEnumerable<Room>> GetAvailable()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Room>> GetRoomsOfRatOwners()
        {
            throw new NotImplementedException();
        }
    }
}
