using AutoMapper;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using GigHub.Persistence;
using GigHub.Core;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {

        IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var notifications = _unitOfWork.Notificacions
                .GetNewNotifications(User.Identity.GetUserId());

            var notificationsDto = notifications.Select(Mapper.Map<Notification, NotificationDto>).ToList();

            return notificationsDto;
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            _unitOfWork.Notificacions.SetNotificacionsAsRead(User.Identity.GetUserId());
            _unitOfWork.Complete();
            return Ok();
        }
    }
}
