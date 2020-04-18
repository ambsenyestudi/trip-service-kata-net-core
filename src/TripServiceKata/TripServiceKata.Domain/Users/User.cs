using System;
using System.Collections.Generic;
using System.Text;
using TripServiceKata.Domain.Trips;

namespace TripServiceKata.Domain.Users
{
    public class User
    {
        private List<Trip> trips = new List<Trip>();
        private List<User> friends = new List<User>();
        public List<User> GetFriends()
        {
            return friends;
        }

        public void AddFriend(User user)
        {
            friends.Add(user);
        }

        public void AddTrip(Trip trip)
        {
            trips.Add(trip);
        }

        public List<Trip> Trips()
        {
            return trips;
        }

        public bool IsFriendsWith(User user)
        {
            throw new NotImplementedException();
        }
    }
}
