﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using GigHub.ViewModels;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCanceled { get; private set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }

        public DateTime DatetTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }
        public ICollection<Attendance> Attendancees { get; internal set; }

        public Gig()
        {
            Attendancees = new Collection<Attendance>();
        }

        internal void Cancel()
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