using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGigRepository Gigs { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }
        public INotificationRepository Notificacions { get; private set; }
        public IFollowingRepository Followings { get; private set; }

        ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Gigs = new GigRepository(context);
            Genres = new GenreRepository(context);
            Attendances = new AttendanceRepository(context);
            Notificacions = new NotificationRepository(context);
            Followings = new FollowingRepository(context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}