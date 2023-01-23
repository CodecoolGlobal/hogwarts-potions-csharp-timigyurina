namespace HogwartsPotions.Models.Entities
{
    public class Consistency
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public Recipe Recipe { get; set; }
        public Ingredient Ingredient { get; set; }


        // This is a many-to-many join table without payload(or a pure join table) in the database
    }

}
