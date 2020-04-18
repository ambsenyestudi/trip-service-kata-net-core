using System.Collections.Generic;
using TripServiceKata.Domain.Users;

namespace TripServiceKata.Domain.Trips
{
    public interface ITripDAO
    {
        List<Trip> GetTripsBy(User user);
    }
}