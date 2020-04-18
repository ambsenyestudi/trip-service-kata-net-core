using NSubstitute;
using TripServiceKata.Domain.Exceptions;
using TripServiceKata.Domain.Trips;
using TripServiceKata.Domain.Users;
using TripServiceKata.Test.UserTests;
using Xunit;

namespace TripServiceKata.Test.TripServiceTests
{
    public class trip_service_should
    {
        private static readonly User GUEST = null;
        private static readonly User SOME_USER = new User();
        private static readonly User REGISTERED_USER = new User();
        private static readonly Trip TO_ZARAGOZA = new Trip();
        private static readonly Trip TO_BILBAO = new Trip();
        private readonly UserBuilder UserBuilder;
        private TripService productionTripService;
        private IUserSession userSession;
        private ITripDAO tripDAO;
        public trip_service_should()
        {
            userSession = Substitute.For<IUserSession>();
            tripDAO = Substitute.For<ITripDAO>();
            productionTripService = new TripService(userSession, tripDAO);
            UserBuilder = new UserBuilder();
        }

        [Fact]
        public void kick_out_when_user_not_logged_in()
        {
            userSession.GetLoggedUser().Returns(GUEST);

            Assert.Throws<UserNotLoggedInException>(()=> productionTripService.GetTripsByUser(SOME_USER));
        }

        [Fact]
        public void not_show_any_trips_when_users_not_friends()
        {
            var notFriend = UserBuilder
                .WithFriends(SOME_USER)
                .WithTrips(TO_ZARAGOZA, TO_BILBAO)
                .Build();
            userSession.GetLoggedUser().Returns(REGISTERED_USER);
            
            var trips = productionTripService.GetTripsByUser(notFriend);
            Assert.Empty(trips);
        }
        [Fact]
        public void show_friend_trips()
        {
            var myFriend = UserBuilder
                .WithFriends(SOME_USER, REGISTERED_USER)
                .WithTrips(TO_ZARAGOZA, TO_BILBAO)
                .Build();

            userSession.GetLoggedUser().Returns(REGISTERED_USER);
            tripDAO.GetTripsBy(myFriend).Returns(myFriend.Trips());

            var trips = productionTripService.GetTripsByUser(myFriend);

            var expectedTripCount = 2;
            Assert.Equal(expectedTripCount, trips.Count);
        }
    }
}
