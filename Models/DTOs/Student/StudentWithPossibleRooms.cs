using System.ComponentModel.DataAnnotations;
using HogwartsPotions.Models.DTOs.Room;

namespace HogwartsPotions.Models.DTOs.Student
{
    public class StudentWithPossibleRooms
    {
        [Required]
        public GetStudentDTO Student { get; set; }

        [Required]
        public HashSet<GetRoomDTO> PossibleRooms { get; set; } = new HashSet<GetRoomDTO>();
    }
}
