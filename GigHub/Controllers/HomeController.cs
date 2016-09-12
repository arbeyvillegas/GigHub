using System.Web.Mvc;
using GigHub.Models;
using System.Data.Entity;
using System.Linq;
using System;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _context;


        public HomeController()
        {
            this._context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var upcomingsGigs = _context.Gigs
                .Include(g => g.Artist)
                .Where(g => g.DatetTime > DateTime.Now);

            return View(upcomingsGigs);
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