using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskScheduler.API.Domain.DTOs
{
    public record class RegisterDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; init; }

        [Required]
        [PasswordPropertyText]
        public required string Password { get; init; }

        [Required]
        public required string Username { get; init; }

    }
}