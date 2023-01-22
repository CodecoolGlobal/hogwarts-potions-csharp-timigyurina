using System.ComponentModel.DataAnnotations;
using HogwartsPotions.Models.DTOs.Student;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Models.DTOs.Room
{
    public class GetRoomDTO : BaseRoomDTO // For Get and GetAll
    {
        [Required]
        public int Id { get; set; }

        public int NumberOfResidents { get; }

        [Required]
        public HashSet<StudentDTOWithId> Residents { get; set; }
    }
}
