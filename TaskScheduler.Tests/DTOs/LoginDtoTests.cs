using TaskScheduler.API.Domain.DTOs;

namespace TaskScheduler.Tests.DTOs
{
    public class LoginDtoTests
    {
        [Fact]
        public void Constructor_GivenAllParameters_ThenShouldSetPropertiesCorrectly()
        {
            //Arrange
            var expectedEmail = "test@user.com";
            var expectedPassword = "myverygoodpassword";
            
            //Act
            var loginDto = new LoginDto
            {
                Email = expectedEmail,
                Password = expectedPassword
            };

            //Assert
            Assert.Equal(expectedEmail, loginDto.Email);
            Assert.Equal(expectedPassword, loginDto.Password);

        }
    }
}