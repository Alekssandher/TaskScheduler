using Moq;
using TaskScheduler.API.Domain.DTOs;
using TaskScheduler.API.Domain.Interfaces;
using TaskScheduler.API.Domain.Models;
using TaskScheduler.API.Domain.Repositories;
using TaskScheduler.API.Domain.Services;

namespace TaskScheduler.Tests.Services
{
    [Trait("Category", "Service")]
    public sealed class AccountServiceTests
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IPasswordRepository> _passwordRepoMock;
        private readonly Mock<IJwtRepository> _jwtRepoMock;
        private readonly AccountService _accountService;

        public AccountServiceTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _passwordRepoMock = new Mock<IPasswordRepository>();
            _jwtRepoMock = new Mock<IJwtRepository>();

            _accountService = new AccountService(
                _userRepoMock.Object,
                _passwordRepoMock.Object,
                _jwtRepoMock.Object
            );
        }

        [Fact]
        public async Task Login_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@test.com", Password = "123456" };
            var user = new User { Username = "TestUser", Email = "test@test.com", PasswordHash = "hashedPassword" };

            _userRepoMock.Setup(r => r.GetByEmail(loginDto.Email))
                .ReturnsAsync(user);

            _passwordRepoMock.Setup(r => r.VerifyPassword(loginDto.Password, user.PasswordHash))
                .Returns(true);

            _jwtRepoMock.Setup(r => r.GetJwtToken(user.Email))
                .Returns("fake-jwt-token");

            // Act
            var token = await _accountService.Login(loginDto);

            // Assert
            Assert.Equal("fake-jwt-token", token);
        }

        [Fact]
        public async Task Login_ShouldThrowException_WhenPasswordIsWrong()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@test.com", Password = "wrong" };
            var user = new User { Username = "TestUser" , Email = "test@test.com", PasswordHash = "hashedPassword" };

            _userRepoMock.Setup(r => r.GetByEmail(loginDto.Email))
                .ReturnsAsync(user);

            _passwordRepoMock.Setup(r => r.VerifyPassword(loginDto.Password, user.PasswordHash))
                .Returns(false);

            // Act & Assert
            await Assert.ThrowsAsync < API.Domain.Exceptions.Exceptions.BadRequestException>(
                () => _accountService.Login(loginDto)
            );
        }
        
    }
}