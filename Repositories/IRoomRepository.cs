using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Repositories
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        Task<IEnumerable<Room>> GetAvailable();
        Task<IEnumerable<Room>> GetRoomsOfRatOwners();
        Task<IEnumerable<Room>> GetAvailableOfHouse(HouseType houseType);
        Task<Room?> GetWithDetails(int id);

        //Task<IEnumerable<Room>> SearchRooms(string? houseType, int? capacity, int? numberOfResidents);
    }
}
