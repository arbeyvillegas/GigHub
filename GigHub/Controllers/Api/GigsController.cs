using GigHub.Core;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {

        IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            string userId = User.Identity.GetUserId();

            var gig = _unitOfWork.Gigs.GetGigById(id);

            if (gig == null || gig.IsCanceled)
                return NotFound();

            if (gig.ArtistId != userId)
                return Unauthorized();

            gig.Cancel();

            _unitOfWork.Complete();

            return Ok();
        }

        public string GetUserId()
        {
            return User.Identity.GetUserId();
        }
    }
}
