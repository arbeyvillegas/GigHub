using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GigHub.Core.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCanceled { get; private set; }

        public ApplicationUser Artist { get; set; }

        public string ArtistId { get; set; }

        public DateTime DatetTime { get; set; }

        public string Venue { get; set; }

        public Genre Genre { get; set; }

        public byte GenreId { get; set; }
        public ICollection<Attendance> Attendancees { get; internal set; }

        public Gig()
        {
            Attendancees = new Collection<Attendance>();
        }

        public void Cancel()
        {
            this.IsCanceled = true;

            var notification = Notification.GigCanceled(this);

            foreach (var attendee in this.Attendancees.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        internal void Modify(Gig newGig)
        {
            var notification = Notification.GigUpdated(this, this.DatetTime, this.Venue);

            this.Venue = newGig.Venue;
            this.DatetTime = newGig.DatetTime;
            this.GenreId = newGig.GenreId;

            foreach (var attendee in Attendancees.Select(a => a.Attendee))
                attendee.Notify(notification);
        }
    }
}