using System;
using System.Collections.Generic;
using System.Text;
using TripServiceKata.Domain.Trips;
using TripServiceKata.Domain.Users;

namespace TripServiceKata.Test.UserTests
{
    public class UserBuilder
    {
        private User[] friends = new User[] { };
        private Trip[] trips = new Trip[] { };

        public UserBuilder WithFriends(params User[] friends)
        {
            this.friends = friends;
            return this;
        }
        public UserBuilder WithTrips(params Trip[] trips)
        {
            this.trips = trips;
            return this;
        }
        public User Build()
        {
            var builtUser = new User();
            AddFiredsTo(builtUser);
            AddTripsTo(builtUser);

            return builtUser;
        }

        private void AddFiredsTo(User user)
        {
            foreach (var friend in friends)
            {
                user.AddFriend(friend);
            }
        }

        private void AddTripsTo(User user)
        {
            foreach (var trip in trips)
            {
                user.AddTrip(trip);
            }
        }
    }
}
