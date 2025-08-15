using System.ComponentModel.DataAnnotations;
using TaskScheduler.API.Domain.Enums;

namespace TaskScheduler.API.Domain.DTOs
{
    public class MyTaskRequestDto
    {
        [Required]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; init; } = default!;

        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters.")]
        public string? Description { get; init; }

        public DateTime? FinishDate { get; init; }

        [EnumDataType(typeof(MyTaskStatus), ErrorMessage = "My Task Status.")]
        public MyTaskStatus Status { get; init; }
    }
}