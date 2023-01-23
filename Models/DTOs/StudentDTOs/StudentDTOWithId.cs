using System.ComponentModel.DataAnnotations;

namespace HogwartsPotions.Models.DTOs.StudentDTOs
{
    public class StudentDTOWithId : BaseStudentDTO // for Getting a Room with details -> to avoid circular references
    {
        [Required]
        public int Id { get; set; }
    }
}
