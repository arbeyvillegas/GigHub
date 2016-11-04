using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GigHub.Persistence.Repositories;
using GigHub.Core.Models;
using System.Data.Entity;
using Moq;
using GigHub.Persistence;
using GigHub.Test.Extensions;
using FluentAssertions;

namespace GigHub.Test.Persistence.Repositories
{
    [TestClass]
    public class NotificationRepositoryTests
    {
        NotificationRepository _notificationRepository;
        Mock<DbSet<UserNotification>> _mockUserNotifications;

        [TestInitialize]
        public void TestInitialize()
        {
            //test NotificationRepository.GetNewNotificationsFor()
            _mockUserNotifications = new Mock<DbSet<UserNotification>>();
            var mockDbContext = new Mock<IApplicationDbContext>();
            mockDbContext.Setup(c => c.UserNotifications).Returns(_mockUserNotifications.Object);
            _notificationRepository = new NotificationRepository(mockDbContext.Object);
        }

        [TestMethod]
        public void GetNewNotifications_WhenNotificationIsNotForUser_ShoulNotBeReturned()
        {
            var userNotificacion = CreateUserNotification();


            _mockUserNotifications.SetSource(new[] { userNotificacion });

            var userNotificacions = _notificationRepository.GetNewNotifications("2");

            userNotificacions.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNewNotifications_WhenNotificationIsRead_ShoulNotBeReturned()
        {
            var userNotificacion = CreateUserNotification();
            userNotificacion.Read();

            _mockUserNotifications.SetSource(new[] { userNotificacion });

            var userNotificacions = _notificationRepository.GetNewNotifications("1");

            userNotificacions.Should().BeEmpty();

        }

        [TestMethod]
        public void GetNewNotifications_NotificationIsForTheUser_ShoulBeReturned()
        {
            var userNotificacion = CreateUserNotification();

            _mockUserNotifications.SetSource(new[] { userNotificacion });

            var notifications = _notificationRepository.GetNewNotifications("1");

            notifications.Should().NotBeEmpty();

        }

        private UserNotification CreateUserNotification()
        {
            ApplicationUser user = new ApplicationUser()
            {
                Name = "avillegas"
            };

            Notification notification = Notification.GigCreated(new Gig() { ArtistId = "1", DatetTime = DateTime.Now });

            var userNotificacion = new UserNotification(user, notification);

            userNotificacion.UserId = notification.Gig.ArtistId;

            return userNotificacion;
        }
    }
}
