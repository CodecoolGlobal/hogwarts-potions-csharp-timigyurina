using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public ICollection<Consistency> Consistencies { get; set; }
        public HashSet<PotionIngredient> PotionIngredients { get; set; }
    }

}
