using System.ComponentModel.DataAnnotations;

namespace HogwartsPotions.Models.DTOs.RoomDTOs
{
    public class RoomDTOWithId : BaseRoomDTO // for Getting a Student with details -> to avoid circular references
    {
        [Required]
        public int Id { get; set; }
    }
}
