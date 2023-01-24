using HogwartsPotions.Repositories.Interfaces;

namespace HogwartsPotions.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HogwartsContext _context;

        private readonly IRoomRepository _roomRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IPotionRepository _potionRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IConsistencyRepository _consistencyRepository;
        private readonly IPotionIngredientRepository _potionIngredientRepository;

        private bool _disposed = false;

        public UnitOfWork(
            HogwartsContext context,
            IRoomRepository roomRepository,
            IStudentRepository studentRepository,
            IPotionRepository potionRepository,
            IRecipeRepository recipeRepository,
            IIngredientRepository ingredientRepository,
            IConsistencyRepository consistencyRepository,
            IPotionIngredientRepository potionIngredientRepository)
        {
            _context = context;
            _roomRepository = roomRepository;
            _studentRepository = studentRepository;
            _potionRepository = potionRepository;
            _recipeRepository = recipeRepository;
            _ingredientRepository = ingredientRepository;
            _consistencyRepository = consistencyRepository;
            _potionIngredientRepository = potionIngredientRepository;
        }


        public IRoomRepository RoomRepository
        {
            get
            {
                //if (_roomRepository == null)
                //{
                //    _roomRepository = new RoomRepository(_context);
                //}
                return _roomRepository;
            }
        }

        public IStudentRepository StudentRepository => _studentRepository;

        public IPotionRepository PotionRepository => _potionRepository;

        public IRecipeRepository RecipeRepository => _recipeRepository;

        public IIngredientRepository IngredientRepository => _ingredientRepository;

        public IConsistencyRepository ConsistencyRepository => _consistencyRepository;

        public IPotionIngredientRepository PotionIngredientRepository => _potionIngredientRepository;

        public int Commit()  // use this at adding, updating or deleting an entity
        {
            return _context.SaveChanges();
        }

        public Task<int> CommitAsync() // use this at adding, updating or deleting an entity async
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task DisposeAsync()
        {
            await DisposeAsync(true);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        protected async virtual Task DisposeAsync(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    await _context.DisposeAsync();
                }
            }
            _disposed = true;
        }

    }
}
