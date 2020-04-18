using TripServiceKata.Domain.Users;
using TripServiceKata.Domain.Trips;
using Xunit;
using TripServiceKata.Domain.Exceptions;

namespace TripServiceKata.Test.TripServiceTests
{
    public class trip_service_should
    {
        [Fact]
        public void kick_out_when_user_not_logged_in()
        {
            var someUser = new User();
            var tripService = new TripService();

            Assert.Throws<UserNotLoggedInException>(()=>tripService.GetTripsByUser(someUser));
        }
    }
}
