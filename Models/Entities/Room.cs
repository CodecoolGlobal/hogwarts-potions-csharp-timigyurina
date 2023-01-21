using HogwartsPotions.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HogwartsPotions.Models.Entities
{
    public class Room
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public long ID { get; set; }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public HouseType House { get; set; }

        [Range(0, 10)]
        [Required]
        public int Capacity { get; set; }

        public HashSet<Student> Residents { get; set; } = new HashSet<Student>();
    }
}
