using TaskScheduler.API.Domain.Models;

namespace TaskScheduler.Tests.Models
{
    public class UserTests
    {
        [Fact]
        public void Constructor_GivenAllParameters_ThenShouldSetPropertiesCorrectly()
        {
            //Arrange
            var expectedUsername = "TestUser";
            var expectedEmail = "test@user.com";
            var expectedPasswordHash = "myverygoodpassword";
            var expectedCreatedAt = DateTime.UtcNow;
            var expectedLastLogin = DateTime.UtcNow.AddHours(-2);
            var expectedIsActive = true;

            //Act
            var user = new User
            {
                Username = expectedUsername,
                Email = expectedEmail,
                PasswordHash = expectedPasswordHash,
                CreatedAt = expectedCreatedAt,
                LastLogin = expectedLastLogin,
                IsActive = expectedIsActive
            };

            //Assert
            Assert.Equal(expectedUsername, user.Username);
            Assert.Equal(expectedEmail, user.Email);
            Assert.Equal(expectedPasswordHash, user.PasswordHash);
            Assert.Equal(expectedCreatedAt, user.CreatedAt);
            Assert.Equal(expectedLastLogin, user.LastLogin);
            Assert.Equal(expectedIsActive, user.IsActive);
            
        }
    }
}