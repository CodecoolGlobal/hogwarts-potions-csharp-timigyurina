using HogwartsPotions.Repositories;

namespace HogwartsPotions.Data
{
    public interface IUnitOfWork : IDisposable
    {
        public IRoomRepository RoomRepository { get; }

        int Commit();
        Task<int> CommitAsync();
        Task DisposeAsync();
    }
}
