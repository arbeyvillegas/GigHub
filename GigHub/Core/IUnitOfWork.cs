using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IGenreRepository Genres { get; }
        IGigRepository Gigs { get; }

        void Complete();
    }
}