using TaskScheduler.API.Domain.Enums;
using TaskScheduler.API.Domain.Models;
using TaskScheduler.Tests.Helpers;

namespace TaskScheduler.Tests.Models
{
    public sealed class TaskFilterTests
    {
        [Fact]
        public void Constructor_ShouldSetDefaultValues()
        {
            // Act
            var filter = new TaskFilter();

            // Assert
            Assert.Equal(1, filter.Page);
            Assert.Equal(10, filter.PageSize);
            Assert.Null(filter.Status);
            Assert.Null(filter.FromDate);
            Assert.Null(filter.ToDate);
            Assert.Null(filter.TitleContains);
        }
        
        [Fact]
        public void TitleContains_WhenTooLong_ShouldFailValidation()
        {
            // Arrange
            var filter = new TaskFilter
            {
                TitleContains = new string('A', 201) // > 200
            };

            // Act
            var results = ValidationHelper.Validate(filter);

            // Assert
            Assert.Contains(results, r => r.ErrorMessage!.Contains("Title search cannot exceed"));
        }

        [Fact]
        public void Page_WhenLessThanOne_ShouldFailValidation()
        {
            var filter = new TaskFilter { Page = 0 };

            var results = ValidationHelper.Validate(filter);

            Assert.Contains(results, r => r.ErrorMessage!.Contains("Page must be greater"));
        }

        [Fact]
        public void PageSize_WhenGreaterThan100_ShouldFailValidation()
        {
            var filter = new TaskFilter { PageSize = 200 };

            var results = ValidationHelper.Validate(filter);

            Assert.Contains(results, r => r.ErrorMessage!.Contains("PageSize must be between"));
        }

        [Fact]
        public void ValidFilter_ShouldPassValidation()
        {
            var filter = new TaskFilter
            {
                Status = MyTaskStatus.ToDo,
                FromDate = DateTime.UtcNow.AddDays(-1),
                ToDate = DateTime.UtcNow.AddDays(1),
                TitleContains = "Meeting",
                Page = 2,
                PageSize = 20
            };

            var results = ValidationHelper.Validate(filter);

            Assert.Empty(results); 
        }
    }
}