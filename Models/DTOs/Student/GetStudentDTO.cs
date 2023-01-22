using System.ComponentModel.DataAnnotations;
using HogwartsPotions.Models.DTOs.Room;

namespace HogwartsPotions.Models.DTOs.Student
{
    public class GetStudentDTO : BaseStudentDTO // For Get and GetAll
    {
        [Required]
        public int Id { get; set; }


        public int? RoomId { get; set; }
        public RoomDTOWithId? Room { get; set; }
    }
}
