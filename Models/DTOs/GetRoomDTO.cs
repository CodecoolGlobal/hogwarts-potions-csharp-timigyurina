using System.ComponentModel.DataAnnotations;

namespace HogwartsPotions.Models.DTOs
{
    public class GetRoomDTO : BaseRoomDTO // For Get and GetAll
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public HashSet<GetStudentDTO> Residents { get; set; }
    }
}
