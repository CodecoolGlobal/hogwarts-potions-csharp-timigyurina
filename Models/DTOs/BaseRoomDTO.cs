using HogwartsPotions.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace HogwartsPotions.Models.DTOs
{
    public abstract class BaseRoomDTO
    {
        [Range(0, 10)]
        [Required]
        public int Capacity { get; set; }

        [Required]
        public string? House { get; set; }

    }
}
