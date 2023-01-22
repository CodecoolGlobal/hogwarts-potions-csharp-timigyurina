using HogwartsPotions.Data;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Repositories
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(HogwartsContext context) : base(context)
        {
        }

        public Task<IEnumerable<Room>> GetAvailable()
        {
            return GetAllAsync(r => r.Residents.Count < r.Capacity);
        }

        public Task<IEnumerable<Room>> GetRoomsOfRatOwners()
        {
           return GetAllAsync(r => r.Residents.All(res => res.PetType == PetType.None || res.PetType == PetType.Rat)); 
        }
    }
}
