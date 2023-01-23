using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Data
{
    public static class DbInitialiser
    {
        public static void Initialise(HogwartsContext context)
        {
            context.Database.EnsureCreated();

            if (context.Ingredients.Any())
            {
                return;   
            }

            Ingredient[] ingredients = new Ingredient[]
            {
                new Ingredient(){ Name = "water" },
                new Ingredient(){ Name = "earth" },
                new Ingredient(){ Name = "salt" },
                new Ingredient(){ Name = "mud" },
                new Ingredient(){ Name = "grass" },
                new Ingredient(){ Name = "oakleaf" },
                new Ingredient(){ Name = "gold" },
                new Ingredient(){ Name = "ash" },
                new Ingredient(){ Name = "pumpkin" },
                new Ingredient(){ Name = "apple" }
            };


            foreach (Ingredient ingredient in ingredients)
            {
                context.Ingredients.Add(ingredient);
            }
            context.SaveChanges();
        }
    }
}
