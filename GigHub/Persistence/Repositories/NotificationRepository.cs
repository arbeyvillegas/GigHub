using GigHub.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Notification> GetNewNotifications(string userId)
        {
            return _context.UserNotifications
                .Where(u => u.UserId == userId && u.IsRead == false)
                .Select(u => u.Notification)
                .Include(u => u.Gig.Artist)
                .ToList();
        }

        public void SetNotificacionsAsRead(string userId)
        {
            var userNotifications = _context.UserNotifications
                .Where(u => u.UserId == userId && u.IsRead == false)
                .Select(u => u)
                .ToList();

            userNotifications.ForEach(u => u.Read());
        }
    }
}