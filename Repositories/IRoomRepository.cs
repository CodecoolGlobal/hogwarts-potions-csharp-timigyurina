using HogwartsPotions.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Repositories
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        Task<IEnumerable<Room>> GetAvailable();
        Task<IEnumerable<Room>> GetRoomsOfRatOwners();
        //Task<Room?> AssignTo(int roomId, Student student);
        //Task<bool> RemoveStudentFrom(int originalRoomIdOfStudent, Student student);
        //Task<IEnumerable<Room>> SearchRooms(string? houseType, int? capacity, int? numberOfResidents);
    }
}
