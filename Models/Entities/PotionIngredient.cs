namespace HogwartsPotions.Models.Entities
{
    public class PotionIngredient
    {
        public int PotionId { get; set; }
        public int IngredientId { get; set; }
        public Potion Potion { get; set; }
        public Ingredient Ingredient { get; set; }


        // Also a many-to-many join table without payload(or a pure join table) in the database
    }

}
