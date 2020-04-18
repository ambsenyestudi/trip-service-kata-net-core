using System;
using System.Collections.Generic;
using System.Text;
using TripServiceKata.Domain.Exceptions;
using TripServiceKata.Domain.Users;

namespace TripServiceKata.Domain.Trips
{
    public class TripDAO
    {
        public static List<Trip> FindTripsByUser(User user)
        {
            throw new DependendClassCallDuringUnitTestException(
                        "TripDAO should not be invoked on an unit test.");
        }
    }
}
