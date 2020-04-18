using System;

namespace TripServiceKata.Domain.Exceptions
{
    [Serializable]
    public class UserNotLoggedInException : System.Exception
    {
        public UserNotLoggedInException()
        {

        }

        public UserNotLoggedInException(string message) : base(message)
        {

        }
    }
}
