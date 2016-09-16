using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                .Where(g => g.ArtistId == userId &&
                    g.DatetTime >= DateTime.Now &&
                    g.IsCanceled == false)
                .Include(g => g.Genre)
                .ToList();

            return View(gigs);
        }


        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var gigs = _context.Attendences
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending"
            };

            return View("Gigs", viewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel()
            {
                Genres = _context.Genres.ToList(),
                Heading = "Create a Gig"
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            var viewModel = new GigFormViewModel()
            {
                Id = gig.Id,
                Genres = _context.Genres.ToList(),
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
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var gig = new Gig()
            {
                ArtistId = User.Identity.GetUserId(),
                DatetTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            string userId = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Include(g => g.Attendancees.Select(a => a.Attendee))
                .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

            var newGig = new Gig()
            {
                GenreId = viewModel.Genre,
                DatetTime = viewModel.GetDateTime(),
                Venue = viewModel.Venue
            };

            gig.Modify(newGig);

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }
    }
}