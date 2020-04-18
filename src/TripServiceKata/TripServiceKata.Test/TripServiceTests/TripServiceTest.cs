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
        private static readonly User SOME_USER = null;
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
