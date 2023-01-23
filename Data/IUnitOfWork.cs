using HogwartsPotions.Repositories;

namespace HogwartsPotions.Data
{
    public interface IUnitOfWork : IDisposable
    {
        public IRoomRepository RoomRepository { get; }
        public IStudentRepository StudentRepository { get; }
        public IPotionRepository PotionRepository { get; }

        int Commit();
        Task<int> CommitAsync();
        Task DisposeAsync();
    }
}
