using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

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


        [HttpGet]
        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            string userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(u => u.UserId == userId && u.IsRead == false)
                .Select(u => u.Notification)
                .Include(u => u.Gig.Artist)
                .ToList();

            var notificationsDto = notifications.Select(Mapper.Map<Notification, NotificationDto>).ToList();

            return notificationsDto;
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            string userId = User.Identity.GetUserId();
            var userNotifications = _context.UserNotifications
                .Where(u => u.UserId == userId && u.IsRead == false)
                .Select(u => u)
                .ToList();

            userNotifications.ForEach(u => u.Read());

            int result = _context.SaveChanges();

            return Ok();
        }
    }
}
