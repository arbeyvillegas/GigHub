using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingController : ApiController
    {
        IUnitOfWork _unitOfWork;

        public FollowingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_unitOfWork.Followings.Exists(userId, dto.FolloweeId))
                return BadRequest("Following already exists.");

            var following = new Following()
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            _unitOfWork.Followings.Add(following);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult StopFollow(string id)
        {
            var userId = User.Identity.GetUserId();

            var following = _unitOfWork.Followings.Get(userId, id);

            if (following != null)
            {
                _unitOfWork.Followings.Remove(following);

                _unitOfWork.Complete();
            }

            return Ok(id);
        }
    }
}
