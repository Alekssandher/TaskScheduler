using TaskScheduler.API.Domain.Enums;

namespace TaskScheduler.API.Domain.Models
{
    public class TaskFilter
    {
        public MyTaskStatus? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? TitleContains { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}