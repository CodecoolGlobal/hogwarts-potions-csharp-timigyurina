using System.ComponentModel.DataAnnotations;

namespace HogwartsPotions.Models.DTOs
{
    public abstract class BaseStudentDTO 
    {
        [StringLength(40, MinimumLength = 2)]
        [Required]
        public string? Name { get; set; }

        public string? HouseType { get; set; }
        public string? PetType { get; set; }
    }
}
