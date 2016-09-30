using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Exists(string followerId, string followeeId)
        {
            return _context
                .Followings
                .Any(f => f.FolloweeId == followeeId
                && f.FollowerId == followerId);
        }

        public Following Get(string followerId,string followeeId)
        {
            return _context
                .Followings
                .SingleOrDefault(f => f.FolloweeId == followeeId
                && f.FollowerId == followerId);
        }

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }

        public void Remove(Following following)
        {
            _context.Followings.Remove(following);
        }
    }
}