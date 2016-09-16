using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

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
    }

}

