using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        Gig GetGigById(int id);
        IEnumerable<Gig> GetGigsByUserIdWithGenre(string userId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        IEnumerable<Gig> GetUpcomingGigs(string query);
        void Update(int id, string userId, Gig newGig);
    }
}