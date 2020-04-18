using TripServiceKata.Domain.Exceptions;

namespace TripServiceKata.Domain.Users
{
    public class UserSession : IUserSession
    {
        private static readonly UserSession userSession = new UserSession();
        public static UserSession GetInstance() => userSession;
        public bool IsUserLoggedIn(User user)
        {
            throw new DependendClassCallDuringUnitTestException(
                "UserSession.IsUserLoggedIn() should not be called in an unit test");
        }

        public User GetLoggedUser()
        {
            throw new DependendClassCallDuringUnitTestException(
                "UserSession.GetLoggedUser() should not be called in an unit test");
        }
    }
}
