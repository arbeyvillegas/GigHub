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

            if (gig.ArtistId != userId)
                return Unauthorized();

            if (gig != null && !gig.IsCanceled)
            {

                gig.Cancel();

                _unitOfWork.Complete();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }
}
