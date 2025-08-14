using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskScheduler.API.Domain.DTOs
{
    public record class LoginDto
    {
        [Required(ErrorMessage = "Email Field Is Required.")]
        [EmailAddress]
        public required string Email { get; init; }

        [Required(ErrorMessage = "Password Field Is Required")]
        [PasswordPropertyText]
        public required string Password { get; init; }
    }
}