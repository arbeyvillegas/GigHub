using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Repositories
{
    public class AttendanceRepository
    {

        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendences
                            .Where(a => a.AttendeeId == userId && a.Gig.DatetTime > DateTime.Now)
                            .ToList();
        }
    }
}