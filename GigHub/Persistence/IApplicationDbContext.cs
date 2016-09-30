using System.Data.Entity;
using GigHub.Core.Models;

namespace GigHub.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Attendance> Attendences { get; set; }
        DbSet<Following> Followings { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Gig> Gigs { get; set; }
        DbSet<Notification> Notification { get; set; }
        DbSet<UserNotification> UserNotifications { get; set; }
    }
}