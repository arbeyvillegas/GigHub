using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GigHub.Controllers.Api;
using GigHub.Core.Repositories;
using Moq;
using GigHub.Core;
using GigHub.Test.Extensions;
using GigHub.Core.Dtos;
using System.Web.Http.Results;
using GigHub.Core.Models;

namespace GigHub.Test.Controllers.Api
{
    [TestClass]
    public class AttendancesControllerTests
    {

        private AttendancesController _controller;
        private Mock<IAttendanceRepository> _mockRepository;
        private readonly string _userId = "1";


        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IAttendanceRepository>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Attendances).Returns(_mockRepository.Object);

            _controller = new AttendancesController(mockUnitOfWork.Object);
            _controller.MockCurrentUser(_userId, "arbeyvillegas@gmail.com");
        }

        [TestMethod]
        public void Attend_AttendanceExistsWithGivenId_ShouldBeBadRequest()
        {
            var attendanceDto = new AttendanceDto();
            attendanceDto.GigId = 1;

            _mockRepository.Setup(r => r.Exists(attendanceDto.GigId, _userId)).Returns(true);

            var result = _controller.Attend(attendanceDto);

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Attend_ValidRequest_ShouldReturnOk()
        {
            var attendanceDto = new AttendanceDto();
            attendanceDto.GigId = 1;

            var result = _controller.Attend(attendanceDto);

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void NoAttend_WithInvalidAttendId_ShouldReturnNotFound()
        {
            int gigId = 1;

            var gig = new Attendance()
            {
                GigId = gigId,
                AttendeeId = _userId
            };

            _mockRepository.Setup(r => r.Get(gigId, _userId)).Returns(gig);

            var result = _controller.NoAttend(gigId + 1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void NoAttend_ValidRequest_ShouldReturnOkResult()
        {
            int gigId = 1;

            var attendance = new Attendance()
            {
                GigId = gigId,
                AttendeeId = _userId
            };

            _mockRepository.Setup(r => r.Get(gigId, _userId)).Returns(attendance);

            var result = _controller.NoAttend(gigId);
            result.Should().BeOfType<OkNegotiatedContentResult<int>>();
        }
    }
}
