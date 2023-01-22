using System.ComponentModel.DataAnnotations;

namespace HogwartsPotions.Models.DTOs.Student
{
    public class StudentAssignDTO
    {
        [Required]
        public int RoomId { get; set; }
    }
}
