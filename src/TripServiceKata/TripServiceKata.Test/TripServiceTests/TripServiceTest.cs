using TripServiceKata.Domain.Users;
using TripServiceKata.Domain.Trips;
using Xunit;
using TripServiceKata.Domain.Exceptions;

namespace TripServiceKata.Test.TripServiceTests
{
    public class trip_service_should
    {
        private static User loggedInUser = null;
        private static readonly User GUEST = null;
        private static readonly User SOME_USER = new User();
        private static readonly User REGISTERED_USER = new User();
        private static readonly User NOT_FRIEND_USER = new User();
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

            var user = new User();
            user.AddFriend(NOT_FRIEND_USER);
            user.AddTrip(TO_ZARAGOZA);
            user.AddTrip(TO_BILBAO);
            var trips = tripService.GetTripsByUser(user);
            Assert.Empty(trips);
        }

        //Sim testing class to avoid jumping to DI at first step
        class TestableTripService : TripService
        {
            protected override User GetLoggedUsers()
            {
                return loggedInUser;
            }
        }
    }
}
