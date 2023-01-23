using System.ComponentModel.DataAnnotations;

namespace HogwartsPotions.Models.DTOs.StudentDTOs
{
    public class StudentAssignDTO
    {
        [Required]
        public int RoomId { get; set; }
    }
}
