using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Repositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<Student?> AssignRoomTo(int id, int roomId);
        Task<Student?> GetWithDetails(int id);
       
        //Task<IEnumerable<Student>> SearchStudents(string? houseType, string? petType, int? roomId);
    }
}
