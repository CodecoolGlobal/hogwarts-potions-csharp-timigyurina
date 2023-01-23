using System.ComponentModel.DataAnnotations;

namespace HogwartsPotions.Models.DTOs.StudentDTOs
{
    public abstract class BaseStudentDTO
    {
        [StringLength(40, MinimumLength = 2)]
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? HouseType { get; set; }
        [Required]
        public string? PetType { get; set; }
    }
}
