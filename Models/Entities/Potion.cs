using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Models.Entities
{
    public class Potion
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        public BrewingStatus BrewingStatus { get; set; }


        [ForeignKey(nameof(StudentId))]
        [Required]
        public int StudentId { get; set; }
        public Student Student { get; set; }


        [ForeignKey(nameof(RecipeId))]
       // [Required]
        public int? RecipeId { get; set; }
        public Recipe? Recipe { get; set; }

        
        public HashSet<PotionIngredient> PotionIngredients { get; set; }
    }
}
