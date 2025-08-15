using TaskScheduler.API.Domain.Enums;

namespace TaskScheduler.API.Domain.DTOs
{
    public class MyTaskResponse
    {
        public int Id { get; init; }

        public required string Title { get; init; }

        public string? Description { get; init; }

        public DateTime ScheduledAt { get; init; }
        public DateTime FinishDate { get; init; }

        public MyTaskStatus Status { get; init; }

        public DateTime CreatedAt { get; init; }

        public DateTime? UpdatedAt { get; init; }
    }
}