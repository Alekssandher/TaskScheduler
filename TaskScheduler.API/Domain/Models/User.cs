using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskScheduler.API.Domain.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; init; }

        [StringLength(100)]
        public required string Username { get; init; }

        [StringLength(255)]
        public required string Email { get; init; }

        [StringLength(255)]
        public required string PasswordHash { get; init; }

        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

        public DateTime? LastLogin { get; init; }

        public long? TelegramChatId { get; init; }

        public bool IsActive { get; init; }
    }
}