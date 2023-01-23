using System.ComponentModel.DataAnnotations;
using HogwartsPotions.Models.DTOs.RoomDTOs;

namespace HogwartsPotions.Models.DTOs.StudentDTOs
{
    public class StudentWithPossibleRooms
    {
        [Required]
        public GetStudentDTO Student { get; set; }

        [Required]
        public HashSet<GetRoomDTO> PossibleRooms { get; set; } = new HashSet<GetRoomDTO>();
    }
}
