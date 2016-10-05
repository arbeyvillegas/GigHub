using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GigHub.Persistence.Repositories;
using GigHub.Persistence;
using Moq;
using GigHub.Core.Models;
using System.Data.Entity;
using GigHub.Test.Extensions;
using System.Collections.Generic;
using FluentAssertions;

namespace GigHub.Test.Persistence.Repositories
{
    [TestClass]
    public class GigRepositoryTests
    {
        private GigRepository _repository;
        private Mock<DbSet<Gig>> _mockGigs;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockGigs = new Mock<DbSet<Gig>>();
            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);
            _repository = new GigRepository(mockContext.Object);
        }

        [TestMethod]
        public void GeUpcomingGigsByArtist_GigIsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig()
            {
                DatetTime = DateTime.Now.AddDays(-1),
                ArtistId="1"
            };

            _mockGigs.SetSource(new [] { gig });

            var gigs = _repository.GetGigsByUserIdWithGenre("1");

            gigs.Should().BeEmpty();
        }
    }
}
