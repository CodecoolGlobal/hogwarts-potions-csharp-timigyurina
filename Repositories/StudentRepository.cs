using HogwartsPotions.Data;
using HogwartsPotions.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(HogwartsContext context) : base(context)
        {
        }

        public async Task<Student?> AssignRoomTo(int id, int roomId)
        {
            Student? studentToBeAssignedTo = await GetAsync(id);
            if (studentToBeAssignedTo == null)
            {
                return null;
            }

            studentToBeAssignedTo.RoomId = roomId;
            await UpdateAsync(studentToBeAssignedTo);
            return studentToBeAssignedTo;
        }

        public async Task<Student?> GetWithDetails(int id)
        {
            return await _context.Students.Include(q => q.Room)
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}
