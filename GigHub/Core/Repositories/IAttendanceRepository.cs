using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        bool Exists(int gigId, string attendeeId);
        Attendance Get(int gigId, string attendeeId);
        void Add(Attendance attendance);
        void Remove(Attendance attendance);
    }
}