using System.Web.Mvc;
using GigHub.Core.Models;
using System.Data.Entity;
using System.Linq;
using System;
using GigHub.Core.ViewModels;
using Microsoft.AspNet.Identity;
using GigHub.Persistence.Repositories;
using GigHub.Persistence;
using GigHub.Core;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        IUnitOfWork _unityOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unityOfWork = unitOfWork;
        }

        public ActionResult Index(string query = null)
        {
            var upcomingsGigs = _unityOfWork.Gigs.GetUpcomingGigs(query);

            string userId = User.Identity.GetUserId();
            var attendances = _unityOfWork.Attendances
                .GetFutureAttendances(userId)
                .ToLookup(a => a.Gig.Id);

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = upcomingsGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                Atendances = attendances
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