using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using TaskScheduler.API.Domain.DTOs;
using TaskScheduler.API.Domain.Interfaces;
using TaskScheduler.API.Domain.Mappers;
using TaskScheduler.API.Domain.Models;
using TaskScheduler.API.Domain.Repositories;
using TaskScheduler.API.Domain.Services;

namespace TaskScheduler.Tests.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _taskRepoMock;
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessor;
        private readonly Mock<IJwtRepository> _jwtRepoMock;
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _taskRepoMock = new Mock<ITaskRepository>();
            _jwtRepoMock = new Mock<IJwtRepository>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();

            _taskService = new TaskService(
                _taskRepoMock.Object,
                _userRepoMock.Object,
                _httpContextAccessor.Object,
                _jwtRepoMock.Object
            );
        }

        private void SetupHttpContextWithToken(string token, string email)
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Authorization = $"Bearer {token}";
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

            _jwtRepoMock.Setup(j => j.ExtractEmail(token)).Returns(email);
        }

        [Fact]
        public async Task GetAllTasksWithNoFilters_ShouldReturnAllTasks()
        {
            // Arrange
            SetupHttpContextWithToken("fake-jwt-token", "test@test.com");

            var user = new User
            {
                Id = 1,
                Username = "TestUser",
                Email = "test@test.com",
                PasswordHash = "password"
            };

            var tasks = new List<MyTask>
            {
                new() { Id = 1, Title = "Task 1", UserId = 1 },
                new() { Id = 2, Title = "Task 2", UserId = 1 }
            };

            var taskFilter = new TaskFilter();

            _userRepoMock.Setup(r => r.GetByEmail("test@test.com"))
                .ReturnsAsync(user);

            _taskRepoMock.Setup(r => r.GetTasksByFilters(user.Id, taskFilter))
                .ReturnsAsync(tasks);

            // Act
            var result = await _taskService.GetAllTasks(taskFilter);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, t => t.Title == "Task 1");
        }

        [Fact]
        public async Task CreateTask_ShouldReturnNothing_WhenDtoIsValid()
        {
            SetupHttpContextWithToken("fake-jwt-token", "test@test.com");

            var user = new User
            {
                Id = 1,
                Username = "TestUser",
                Email = "test@test.com",
                PasswordHash = "password"
            };

            var taskDto = new MyTaskRequestDto
            {
                Title = "DoTheDishes",
                Description = "Do the dishes before mom gets home.",
                FinishDate = DateTime.UtcNow.AddHours(2),
                Status = API.Domain.Enums.MyTaskStatus.ToDo
            };

            _taskRepoMock.Setup(r => r.AddTask(It.IsAny<MyTask>())).Returns(Task.CompletedTask);
            
            _userRepoMock.Setup(r => r.GetByEmail("test@test.com"))
                .ReturnsAsync(user);

            await _taskService.CreateTask(taskDto);

            _taskRepoMock.Verify(r => r.AddTask(It.Is<MyTask>(
                t => t.Title == "DoTheDishes" && t.UserId == 1
            )), Times.Once);
        }
    }
}