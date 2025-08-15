using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskScheduler.API.Domain.Enums;

namespace TaskScheduler.API.Domain.Models
{
    public record class MyTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; init; }

        [StringLength(200)]
        public required string Title { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }

        public DateTime ScheduledAt { get; init; } = DateTime.UtcNow;
        public DateTime? FinishDate { get; set; }

        public MyTaskStatus Status { get; set; } = MyTaskStatus.ToDo;

        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; init; }
        public User User { get; init; } = default!;
    }
}