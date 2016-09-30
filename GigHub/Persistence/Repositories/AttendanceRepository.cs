using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {

        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Attendance attendance)
        {
            _context.Attendences.Add(attendance);
        }

        public bool Exists(int gigId, string attendeeId)
        {
            return _context
                .Attendences
                .Any(a => a.GigId == gigId 
                && a.AttendeeId == attendeeId);
        }

        public Attendance Get(int gigId, string attendeeId)
        {
            return _context
                .Attendences
                .SingleOrDefault(a => a.GigId == gigId
                && a.AttendeeId == attendeeId);
        }

        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendences
                            .Include(a => a.Gig)
                            .Where(a => a.AttendeeId == userId && a.Gig.DatetTime > DateTime.Now)
                            .ToList();
        }

        public void Remove(Attendance attendance)
        {
            _context.Attendences.Remove(attendance);
        }
    }
}