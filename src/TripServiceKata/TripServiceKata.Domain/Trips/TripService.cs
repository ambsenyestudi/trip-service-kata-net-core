using System;
using System.Collections.Generic;
using Tools.Assertions;
using TripServiceKata.Domain.Exceptions;
using TripServiceKata.Domain.Users;

namespace TripServiceKata.Domain.Trips
{
    public class TripService
    {
        private readonly IUserSession userSession;
        private readonly ITripDAO tripDAO;

        public TripService(IUserSession userSession, ITripDAO tripDAO)
        {
            this.userSession = userSession ?? throw new ArgumentNullException("No user session at trip service");
            this.tripDAO = tripDAO ?? throw new ArgumentNullException("No tripDAO at trip service");
        }
        public List<Trip> GetTripsByUser(User user)
        {
            User loggedUser = userSession.GetLoggedUser();
            Ensure.NotNull<UserNotLoggedInException>(loggedUser, "User is not logged in");

            return user.IsFriendsWith(loggedUser)
                    ? tripDAO.GetTripsBy(user)
                    : NoTrips();
        }

        private List<Trip> NoTrips() =>
            new List<Trip>();
            
    }
}
