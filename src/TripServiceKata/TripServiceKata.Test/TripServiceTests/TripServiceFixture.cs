using NSubstitute;
using System;
using TripServiceKata.Domain.Trips;
using TripServiceKata.Domain.Users;
using TripServiceKata.Test.UserTests;

namespace TripServiceKata.Test.TripServiceTests
{
    public class TripServiceFixture : IDisposable
    {

        public IUserSession UserSession { get; }
        public ITripDAO TripDAO { get; }
        public TripService TripService { get; }
        public UserBuilder UserBuilder { get; }
        public User GUEST { get; }
        public User SOME_USER { get; }
        public User REGISTERED_USER { get; }
        public Trip TO_ZARAGOZA { get; }
        public Trip TO_BILBAO { get; }
        

        public TripServiceFixture()
        {
            GUEST = default(User);
            SOME_USER = new User();
            REGISTERED_USER = new User();
            TO_ZARAGOZA = new Trip();
            TO_BILBAO = new Trip();
            UserBuilder = new UserBuilder();
            UserSession = Substitute.For<IUserSession>();
            TripDAO = Substitute.For<ITripDAO>();
            TripService = new TripService(UserSession, TripDAO);
        }

        public void Dispose()
        {
        }
    }
}
