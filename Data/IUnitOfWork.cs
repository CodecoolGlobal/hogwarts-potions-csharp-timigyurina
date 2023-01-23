using HogwartsPotions.Repositories.Interfaces;

namespace HogwartsPotions.Data
{
    public interface IUnitOfWork : IDisposable
    {
        public IRoomRepository RoomRepository { get; }
        public IStudentRepository StudentRepository { get; }
        public IPotionRepository PotionRepository { get; }
        public IRecipeRepository RecipeRepository { get; }
        public IIngredientRepository IngredientRepository { get; }
        public IConsistencyRepository ConsistencyRepository { get; }

        int Commit();
        Task<int> CommitAsync();
        Task DisposeAsync();
    }
}
