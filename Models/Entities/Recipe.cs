using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HogwartsPotions.Models.Entities
{
    public class Recipe
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }


        [ForeignKey(nameof(StudentId))]
        [Required]
        public int StudentId { get; set; }
        public Student Student { get; set; }


        public HashSet<Ingredient> Ingredients { get; set; } = new HashSet<Ingredient>();
    }
}
