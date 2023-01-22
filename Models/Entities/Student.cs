using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Models.Entities
{
    public class Student
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public long ID { get; set; }

        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(40, MinimumLength = 2)]
        [Required]
        public string Name { get; set; }

        [Required]
        public HouseType HouseType { get; set; }

        [Required]
        public PetType PetType { get; set; }

        [ForeignKey(nameof(RoomId))]
        public int? RoomId { get; set; }
        public Room? Room { get; set; }
    }
}
