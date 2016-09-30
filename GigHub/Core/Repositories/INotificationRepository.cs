using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNewNotifications(string userId);
        void SetNotificacionsAsRead(string userId);
    }
}