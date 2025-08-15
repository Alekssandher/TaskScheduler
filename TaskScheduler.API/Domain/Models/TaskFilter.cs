using System.ComponentModel.DataAnnotations;
using TaskScheduler.API.Domain.Enums;

namespace TaskScheduler.API.Domain.Models
{
    public class TaskFilter
    {
        [EnumDataType(typeof(MyTaskStatus), ErrorMessage = "Invalid status value.")]
        public MyTaskStatus? Status { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid start date format.")]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid end date format.")]
        public DateTime? ToDate { get; set; }

        [StringLength(200, ErrorMessage = "Title search cannot exceed 200 characters.")]
        public string? TitleContains { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than or equal to 1.")]
        public int Page { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100.")]
        public int PageSize { get; set; } = 10;
    }
}