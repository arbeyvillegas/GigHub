using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Test.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;



namespace GigHub.Test.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private Mock<IGigRepository> _mockRepository;
        private readonly string _userId = "1";

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IGigRepository>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.Gigs).Returns(_mockRepository.Object);

            _controller = new GigsController(mockUnitOfWork.Object);
            _controller.MockCurrentUser(_userId, "arbeyvillegas@gmail.com");
        }


        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.Cancel(5);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigIsCanceled_ShouldReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();

            _mockRepository.Setup(r => r.GetGigById(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_UserCancelAnotherUsersGig_ShouldReturnUnauthorized()
        {
            var gig = new Gig() { ArtistId = _userId + "-" };

            _mockRepository.Setup(r => r.GetGigById(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOk()
        {
            var gig = new Gig() { ArtistId = _userId };

            _mockRepository.Setup(r => r.GetGigById(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<OkResult>();
        }
    }
}
