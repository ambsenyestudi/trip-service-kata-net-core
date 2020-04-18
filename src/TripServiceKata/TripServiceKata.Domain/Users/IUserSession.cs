namespace TripServiceKata.Domain.Users
{
    public interface IUserSession
    {
        User GetLoggedUser();
        bool IsUserLoggedIn(User user);
    }
}