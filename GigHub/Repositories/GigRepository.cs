using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace GigHub.Repositories
{
    public class GigRepository
    {

        private readonly ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Gig> GetGisUserAttending(string userId)
        {
            return _context.Attendences
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }


        public IEnumerable<Gig> GetGigsByUserIdWithGenre(string userId)
        {
            return _context.Gigs
                .Where(g => g.ArtistId == userId &&
                    g.DatetTime >= DateTime.Now &&
                    g.IsCanceled == false)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGigById(int id)
        {
            return _context.Gigs
                            .Include(g => g.Artist)
                            .Include(g => g.Attendancees)
                            .Include(g => g.Artist.Followers)
                            .Single(g => g.Id == id);
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }

        public void Update(int id,string userId,Gig newGig)
        {
            var gig = _context.Gigs
                .Include(g => g.Attendancees.Select(a => a.Attendee))
                .Single(g => g.Id == id && g.ArtistId == userId);

            gig.Modify(newGig);
        }
    }
}