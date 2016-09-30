using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using GigHub.Persistence;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly IApplicationDbContext _context;

        public GigRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
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
                            .Include(g => g.Attendancees.Select(a=>a.Attendee))
                            .Include(g => g.Artist.Followers)
                            .SingleOrDefault(g => g.Id == id);
        }

        public IEnumerable<Gig> GetUpcomingGigs(string query)
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

            return upcomingsGigs;
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }

        public void Update(int id, string userId, Gig newGig)
        {
            var gig = _context.Gigs
                .Include(g => g.Attendancees.Select(a => a.Attendee))
                .Single(g => g.Id == id && g.ArtistId == userId);

            gig.Modify(newGig);
        }
    }
}