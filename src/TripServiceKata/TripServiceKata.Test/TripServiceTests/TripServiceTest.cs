using NSubstitute;
using TripServiceKata.Domain.Exceptions;
using Xunit;

namespace TripServiceKata.Test.TripServiceTests
{
    public class trip_service_should:IClassFixture<TripServiceFixture>
    {
        private TripServiceFixture fixture;

        public trip_service_should(TripServiceFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void kick_out_when_user_not_logged_in()
        {
            fixture.UserSession.GetLoggedUser().Returns(fixture.GUEST);
            var tripService = fixture.TripService;
            Assert.Throws<UserNotLoggedInException>(()=> tripService.GetTripsByUser(fixture.SOME_USER));
        }
        
        [Fact]
        public void not_show_any_trips_when_users_not_friends()
        {
            var notFriend = fixture.UserBuilder
                .WithFriends(fixture.SOME_USER)
                .WithTrips(fixture.TO_ZARAGOZA, fixture.TO_BILBAO)
                .Build();
            fixture.UserSession.GetLoggedUser().Returns(fixture.REGISTERED_USER);
            var tripService = fixture.TripService;

            var trips = tripService.GetTripsByUser(notFriend);
            Assert.Empty(trips);
        }
        
        [Fact]
        public void show_friend_trips()
        {
            var myFriend = fixture.UserBuilder
                .WithFriends(fixture.SOME_USER, fixture.REGISTERED_USER)
                .WithTrips(fixture.TO_ZARAGOZA, fixture.TO_BILBAO)
                .Build();

            fixture.UserSession.GetLoggedUser().Returns(fixture.REGISTERED_USER);
            fixture.TripDAO.GetTripsBy(myFriend).Returns(myFriend.Trips());

            var tripService = fixture.TripService;
            var trips = tripService.GetTripsByUser(myFriend);

            var expectedTripCount = 2;
            Assert.Equal(expectedTripCount, trips.Count);
        }
    }
}
