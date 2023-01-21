using System.ComponentModel.DataAnnotations;

namespace HogwartsPotions.Models.Entities
{
    public class Ingredient
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }
    }
}
