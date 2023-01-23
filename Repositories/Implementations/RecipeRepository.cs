using HogwartsPotions.Data;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Repositories.Interfaces;

namespace HogwartsPotions.Repositories.Implementations
{
    public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(HogwartsContext context) : base(context)
        {
        }
    }
}
