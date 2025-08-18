namespace TaskScheduler.Tests.Exceptions
{
    [Trait("Category", "ExceptionsTests")]
    public sealed class ExceptionsTests
    {
        [Fact]
        public void Constructor_GivenMessage_ThenShouldSetMessageNotFoundException()
        {
            var message = "Task not found.";

            var exception = new API.Domain.Exceptions.Exceptions.NotFoundException(message);

            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void Constructor_GivenMessage_ThenShouldSetMessageBadRequestException()
        {
            var message = "Bad Request Exception.";

            var exception = new API.Domain.Exceptions.Exceptions.BadRequestException(message);

            Assert.Equal(message, exception.Message);
        }
    }
}