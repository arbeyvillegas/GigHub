using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        void Add(Following following);
        bool Exists(string followerId, string followeeId);
        Following Get(string followerId, string followeeId);
        void Remove(Following following);
    }
}