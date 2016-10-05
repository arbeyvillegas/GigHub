using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GigHub.Persistence.Repositories;
using GigHub.Persistence;
using Moq;
using GigHub.Core.Models;
using System.Data.Entity;

namespace GigHub.Test.Persistence.Repositories
{
    [TestClass]
    public class GigRepositoryTests
    {
        private GigRepository _repository;

        [TestInitialize]
        public void TestInitialize()
        {

            var mockGigs = new Mock<DbSet<Gig>>();
            var mockContext = new Mock<IApplicationDbContext>();
            _repository = new GigRepository(mockContext.Object);
        }
    }
}
