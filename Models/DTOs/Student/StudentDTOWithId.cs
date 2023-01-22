using System.ComponentModel.DataAnnotations;

namespace HogwartsPotions.Models.DTOs.Student
{
    public class StudentDTOWithId : BaseStudentDTO // for Getting a Room with details -> to avoid circular references
    {
        [Required]
        public int Id { get; set; }
    }
}
