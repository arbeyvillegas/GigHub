using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistence.Repositories
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
                            .Include(a => a.Gig)
                            .Where(a => a.AttendeeId == userId && a.Gig.DatetTime > DateTime.Now)
                            .ToList();
        }
    }
}