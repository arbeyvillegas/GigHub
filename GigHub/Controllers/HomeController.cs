using System.Web.Mvc;
using GigHub.Models;
using System.Data.Entity;
using System.Linq;
using System;
using GigHub.ViewModels;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _context;


        public HomeController()
        {
            this._context = new ApplicationDbContext();
        }

        public ActionResult Index(string query = null)
        {
            var upcomingsGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DatetTime > DateTime.Now && g.IsCanceled == false);

            if (!String.IsNullOrWhiteSpace(query))
                upcomingsGigs = upcomingsGigs
                    .Where(g => g.Artist.Name.Contains(query) ||
                            g.Genre.Name.Contains(query) ||
                            g.Venue.Contains(query));

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = upcomingsGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query

            };

            return View("Gigs", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}