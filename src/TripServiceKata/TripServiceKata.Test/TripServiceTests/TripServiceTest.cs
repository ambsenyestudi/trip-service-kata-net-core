using TripServiceKata.Domain.Users;
using TripServiceKata.Domain.Trips;
using Xunit;
using TripServiceKata.Domain.Exceptions;
using System.Collections.Generic;
using TripServiceKata.Test.UserTests;

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
        private readonly UserBuilder UserBuilder;
        private TestableTripService tripService;
        public trip_service_should()
        {
            tripService = new TestableTripService();
            UserBuilder = new UserBuilder();
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

            var notFriend = UserBuilder
                .WithFriends(SOME_USER)
                .WithTrips(TO_ZARAGOZA, TO_BILBAO)
                .Build();

            var trips = tripService.GetTripsByUser(notFriend);
            Assert.Empty(trips);
        }
        [Fact]
        public void show_friend_trips()
        {
            loggedInUser = REGISTERED_USER;

            var myFriend = UserBuilder
                .WithFriends(SOME_USER, loggedInUser)
                .WithTrips(TO_ZARAGOZA, TO_BILBAO)
                .Build();
            
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
