using System.ComponentModel.DataAnnotations;
using HogwartsPotions.Models.DTOs.RoomDTOs;

namespace HogwartsPotions.Models.DTOs.StudentDTOs
{
    public class GetStudentDTO : BaseStudentDTO // For Get and GetAll
    {
        [Required]
        public int Id { get; set; }


        public int? RoomId { get; set; }
        public RoomDTOWithId? Room { get; set; }
    }
}
