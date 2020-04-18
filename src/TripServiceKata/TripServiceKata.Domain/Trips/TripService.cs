using System.Collections.Generic;
using Tools.Assertions;
using TripServiceKata.Domain.Exceptions;
using TripServiceKata.Domain.Users;

namespace TripServiceKata.Domain.Trips
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User user)
        {
            User loggedUser = GetLoggedUsers();
            Ensure.NotNull<UserNotLoggedInException>(loggedUser, "User is not logged in");

            return user.IsFriendsWith(loggedUser)
                    ? GetTripsBy(user)
                    : NoTrips();
        }
        private List<Trip> NoTrips() =>
            new List<Trip>();
        protected virtual User GetLoggedUsers() =>
            UserSession.GetInstance().GetLoggedUser();
        protected virtual List<Trip> GetTripsBy(User user) =>
            Trips.TripDAO.FindTripsByUser(user);
    }
}
