using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;
using GigHub.Persistence;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Followings.Any(f => f.FolloweeId == dto.FolloweeId && f.FollowerId == userId))
                return BadRequest("Following already exists.");

            var following = new Following()
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult StopFollow(string id)
        {
            var userId = User.Identity.GetUserId();

            var following = _context
                .Followings
                .SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == id);

            if(following != null)
            {
                _context.Followings.Remove(following);

                _context.SaveChanges();
            }

            return Ok(id);
        }
    }
}
