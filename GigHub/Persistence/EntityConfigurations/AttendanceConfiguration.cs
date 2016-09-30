using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfigurations
{
    public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {
            HasKey(a => a.GigId);
            HasKey(a => a.AttendeeId);

            HasKey(a => new {a.GigId,a.AttendeeId });

            Property(a => a.GigId)
                .HasColumnOrder(1);
            Property(a => a.AttendeeId)
                .HasColumnOrder(2);
        }
    }
}