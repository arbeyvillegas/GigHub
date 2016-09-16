using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class Notification
    {

        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }

        [Required]
        public Gig Gig { get; private set; }

        protected Notification()
        {

        }

        private Notification(NotificationType notificationType, Gig gig)
        {

            if (gig == null)
                throw new ArgumentNullException();

            Gig = gig;
            Type = notificationType;
            DateTime = DateTime.Now;

        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(NotificationType.GigCreated, gig);
        }

        public static Notification GigUpdated(Gig newGig, DateTime originalDatetime, string originalVenue)
        {
            var notification = new Notification(NotificationType.GigUpdated, newGig);

            notification.DateTime = originalDatetime;
            notification.OriginalVenue = originalVenue;

            return notification;

        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(NotificationType.GigCanceled, gig);
        }
    }
}