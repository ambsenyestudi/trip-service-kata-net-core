using TripServiceKata.Domain.Users;
using TripServiceKata.Domain.Trips;
using Xunit;
using TripServiceKata.Domain.Exceptions;
using System.Collections.Generic;

namespace TripServiceKata.Test.TripServiceTests
{
    public class trip_service_should
    {
        private static User loggedInUser = null;
        private static readonly User GUEST = null;
        private static readonly User SOME_USER = new User();
        private static readonly User REGISTERED_USER = new User();
        private static readonly Trip TO_ZARAGOZA = new Trip();
        private static readonly Trip TO_BILBAO = new Trip();

        private TestableTripService tripService;
        public trip_service_should()
        {
            tripService = new TestableTripService();
        }

        [Fact]
        public void kick_out_when_user_not_logged_in()
        {
            loggedInUser = GUEST;

            Assert.Throws<UserNotLoggedInException>(()=>tripService.GetTripsByUser(SOME_USER));
        }

        [Fact]
        public void not_show_any_trips_when_users_not_friends()
        {
            loggedInUser = REGISTERED_USER;

            var notFriend = new User();
            notFriend.AddFriend(SOME_USER);
            notFriend.AddTrip(TO_ZARAGOZA);
            notFriend.AddTrip(TO_BILBAO);
            var trips = tripService.GetTripsByUser(notFriend);
            Assert.Empty(trips);
        }
        [Fact]
        public void show_friend_trips()
        {
            loggedInUser = REGISTERED_USER;

            var myFriend = new User();
            myFriend.AddFriend(SOME_USER);
            myFriend.AddFriend(loggedInUser);
            myFriend.AddTrip(TO_BILBAO);
            myFriend.AddTrip(TO_ZARAGOZA);
            
            var trips = tripService.GetTripsByUser(myFriend);

            var expectedTripCount = 2;
            Assert.Equal(expectedTripCount, trips.Count);
        }

        //Sim testing class to avoid jumping to DI at first step
        class TestableTripService : TripService
        {
            protected override User GetLoggedUsers() =>
                loggedInUser;
            protected override List<Trip> GetTripsBy(User user) => 
                user.Trips();
        }
    }
}
