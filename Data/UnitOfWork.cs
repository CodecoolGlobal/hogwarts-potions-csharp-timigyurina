using HogwartsPotions.Repositories;

namespace HogwartsPotions.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HogwartsContext _context;

        private IRoomRepository _roomRepository;
        private IStudentRepository _studentRepository;
        private IPotionRepository _potionRepository;

        private bool _disposed = false;

        public UnitOfWork(HogwartsContext context, IRoomRepository roomRepository, IStudentRepository studentRepository, IPotionRepository potionRepository)
        {
            _context = context;
            _roomRepository = roomRepository;
            _studentRepository = studentRepository;
            _potionRepository = potionRepository;
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

        public IStudentRepository StudentRepository
        {
            get { return _studentRepository; }
        }

        public IPotionRepository PotionRepository
        {
            get { return _potionRepository; }
        }

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
