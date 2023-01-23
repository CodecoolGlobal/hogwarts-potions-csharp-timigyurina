using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Data
{
    public static class DbInitialiser
    {
        public static void Initialise(HogwartsContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Ingredients.Any())
            {
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

            if (!context.Rooms.Any())
            {
                Room[] rooms = new Room[]
                {
                    new Room(){ House = HouseType.Gryffindor, Capacity = 2 },
                    new Room(){ House = HouseType.Slytherin, Capacity = 2 },
                    new Room(){ House = HouseType.Hufflepuff, Capacity = 2 },
                    new Room(){ House = HouseType.Ravenclaw, Capacity = 2 },
                    new Room(){ House = HouseType.Gryffindor, Capacity = 5 },
                    new Room(){ House = HouseType.Slytherin, Capacity = 5 },
                    new Room(){ House = HouseType.Ravenclaw, Capacity = 5 },
                };

                foreach (Room room in rooms)
                {
                    context.Rooms.Add(room);    
                }
                context.SaveChanges();
            }
        }
    }
}
