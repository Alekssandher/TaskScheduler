using TaskScheduler.API.Domain.Enums;
using TaskScheduler.API.Domain.Models;

namespace TaskScheduler.Tests.Models
{
    public sealed class TaskTests
    {
        [Fact]
        public void Constructor_GivenAllParameters_ThenShouldSetPropertiesCorrectly()
        {
            //Arrange
            var expectedUserId = 2;
            var expectedTitle = "Dishes";
            var expectedDescription = "Do the dishes before mom get home.";
            var expectedStatus = MyTaskStatus.ToDo;
            var expectedCreatedAt = DateTime.UtcNow;
            var expectedFinishDate = DateTime.UtcNow.AddHours(2);

            //Act
            var myTask = new MyTask
            {
                Title = expectedTitle,
                Description = expectedDescription,
                UserId = expectedUserId,
                Status = expectedStatus,
                CreatedAt = expectedCreatedAt,
                FinishDate = expectedFinishDate
                
            };

            //Asert
            Assert.Equal(expectedUserId, myTask.UserId);
            Assert.Equal(expectedTitle, myTask.Title);
            Assert.Equal(expectedDescription, myTask.Description);
            Assert.Equal(expectedStatus, myTask.Status);
            Assert.Equal(expectedCreatedAt, myTask.CreatedAt);
            Assert.Equal(expectedFinishDate, myTask.FinishDate);
        }
    }
}