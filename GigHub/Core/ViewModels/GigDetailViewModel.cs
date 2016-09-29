using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Core.ViewModels
{
    public class GigDetailViewModel
    {
        public int Id { get; set; }
        public string ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string Venue { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsFollowing { get; set; }
        public bool IsAttending { get; set; }

    }
}