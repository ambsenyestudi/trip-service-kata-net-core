using System.Collections.Generic;
using TripServiceKata.Domain.Exceptions;
using TripServiceKata.Domain.Users;

namespace TripServiceKata.Domain.Trips
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User user)
        {
            List<Trip> tripList = new List<Trip>();
            User loggedUser = GetLoggedUsers();

            if (loggedUser != null)
            {
                //refator starts at deepest branch
                //this is a feature envy from User domain entity
                //friend is a user responsibility
                /*
                bool isFriend = false;
                foreach (User friend in user.GetFriends())
                {
                    if (friend.Equals(loggedUser))
                    {
                        isFriend = true;
                        break;
                    }
                }
                */
                if (user.IsFriendsWith(loggedUser))
                {
                    tripList = GetTripsBy(user);
                }
                return tripList;
            }
            else
            {
                throw new UserNotLoggedInException();
            }
        }
        protected virtual User GetLoggedUsers() =>
            UserSession.GetInstance().GetLoggedUser();
        protected virtual List<Trip> GetTripsBy(User user) =>
            Trips.TripDAO.FindTripsByUser(user);
    }
}
