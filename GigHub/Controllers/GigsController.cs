using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _unitOfWork.Gigs.GetGigsByUserIdWithGenre(userId);

            return View(gigs);
        }

        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = _unitOfWork.Gigs.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Atendances = _unitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(g => g.GigId),
                Heading = "Gigs I'm Attending"
            };

            return View("Gigs", viewModel);
        }



        [HttpGet]
        public ActionResult Details(int id)
        {
            var gig = _unitOfWork.Gigs.GetGigById(id);

            var gigDetail = new GigDetailViewModel()
            {
                Id = gig.Id,
                Venue = gig.Venue,
                DateTime = gig.DatetTime,
                ArtistId = gig.ArtistId,
                ArtistName = gig.Artist.Name
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                if (gig.Attendancees.Any(a => a.AttendeeId == userId))
                {
                    gigDetail.IsAttending = true;
                }
                if (gig.Artist.Followers.Any(f => f.FollowerId == userId))
                {
                    gigDetail.IsFollowing = true;
                }
            }

            return View(gigDetail);
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel()
            {
                Genres = _unitOfWork.Genres.GetAllGenres(),
                Heading = "Create a Gig"
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigById(id);

            if (gig == null)
                return new HttpNotFoundResult();

            if (gig.ArtistId != userId)
                return new HttpUnauthorizedResult();

            var viewModel = new GigFormViewModel()
            {
                Id = gig.Id,
                Genres = _unitOfWork.Genres.GetAllGenres(),
                Date = gig.DatetTime.ToString("dd/MM/yyyy"),
                Time = gig.DatetTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Heading = "Edit a Gig"

            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetAllGenres();
                return View("GigForm", viewModel);
            }

            var gig = new Gig()
            {
                ArtistId = User.Identity.GetUserId(),
                DatetTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _unitOfWork.Gigs.Add(gig);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetAllGenres();
                return View("GigForm", viewModel);
            }

            string userId = User.Identity.GetUserId();

            var newGig = new Gig()
            {
                GenreId = viewModel.Genre,
                DatetTime = viewModel.GetDateTime(),
                Venue = viewModel.Venue
            };

            _unitOfWork.Gigs.Update(viewModel.Id, userId, newGig);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }
    }
}