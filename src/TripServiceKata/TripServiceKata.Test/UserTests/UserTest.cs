using System;
using System.Collections.Generic;
using System.Text;
using TripServiceKata.Domain.Users;
using Xunit;

namespace TripServiceKata.Test.UserTests
{
    public class user_should
    {
        private readonly User BOB = new User();
        private readonly User ALICE = new User();
        public UserBuilder UserBuilder { get; }

        public user_should()
        {
            UserBuilder = new UserBuilder();
        }

        

        [Fact]
        public void tell_when_user_not_befriended()
        {
            var user = UserBuilder
                .WithFriends(BOB)
                .Build();
            
            Assert.False(user.IsFriendsWith(ALICE));
        }

        [Fact]
        public void tell_when_friendly_user()
        {
            var user = UserBuilder
                .WithFriends(ALICE)
                .Build();

            Assert.True(user.IsFriendsWith(ALICE));
        }
    }
}
