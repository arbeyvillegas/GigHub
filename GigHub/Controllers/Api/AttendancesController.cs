using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{

    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }


        [HttpPost]
        public IHttpActionResult Attend([FromBody] AttendanceDto dto)
        {

            var userId = User.Identity.GetUserId();

            if (_context.Attendences.Any(a => a.GigId == dto.GigId && a.AttendeeId == userId))
                return BadRequest("The attendance already exists.");

            var attendance = new Attendance()
            {
                GigId = dto.GigId,
                AttendeeId = User.Identity.GetUserId()
            };

            _context.Attendences.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult NoAttend(int id)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _context.Attendences.SingleOrDefault(a => a.GigId == id && a.AttendeeId == userId);

            if (attendance == null)
                return NotFound();

            _context.Attendences.Remove(attendance);

            _context.SaveChanges();

            return Ok(id);
        }
    }

}

