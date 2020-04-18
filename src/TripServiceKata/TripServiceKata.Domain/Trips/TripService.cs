using System.Collections.Generic;
using TripServiceKata.Domain.Exceptions;
using TripServiceKata.Domain.Users;

namespace TripServiceKata.Domain.Trips
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User user)
        {
            User loggedUser = GetLoggedUsers();

            if (loggedUser != null)
            {
                if (user.IsFriendsWith(loggedUser))
                {
                    return GetTripsBy(user);
                }
                return NoTrips();
            }
            else
            {
                throw new UserNotLoggedInException();
            }
        }
        private List<Trip> NoTrips() =>
            new List<Trip>();
        protected virtual User GetLoggedUsers() =>
            UserSession.GetInstance().GetLoggedUser();
        protected virtual List<Trip> GetTripsBy(User user) =>
            Trips.TripDAO.FindTripsByUser(user);
    }
}
