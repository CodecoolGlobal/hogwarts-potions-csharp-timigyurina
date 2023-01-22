using HogwartsPotions.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace HogwartsPotions.Models.DTOs
{
    public class GetStudentDTO : BaseStudentDTO // For Get and GetAll
    {
        [Required]
        public int Id { get; set; }

        public int? RoomId { get; set; }
        public Room? Room { get; set; }
    }
}
