using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfigurations
{
    public class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfiguration()
        {
            HasKey(u => new { u.UserId, u.NotificationId });

            Property(u => u.UserId).HasColumnOrder(1);
            Property(u => u.NotificationId).HasColumnOrder(2);
        }
    }
}