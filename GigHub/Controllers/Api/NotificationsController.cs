using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            string userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(u => u.UserId == userId)
                .Select(u => u.Notification)
                .Include(u => u.Gig.Artist)
                .ToList();

            return notifications.Select(n => new NotificationDto()
            {
                DateTime = n.DateTime,
                Gig = new GigDto()
                {
                    Artist = new UserDto
                    {
                        Id = n.Gig.Artist.Id,
                        Name = n.Gig.Artist.Name
                    },
                    DatetTime = n.Gig.DatetTime,
                    Venue = n.Gig.Venue,
                    Id = n.Gig.Id
                },
                OriginalDateTime = n.OriginalDateTime,
                OriginalVenue = n.OriginalVenue,
                Type = n.Type

            });
        }
    }
}
