using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IGenreRepository Genres { get; }
        IGigRepository Gigs { get; }
        IAttendanceRepository Attendances { get; }
        INotificationRepository Notificacions { get; }
        IFollowingRepository Followings { get; }

        int Complete();
    }
}