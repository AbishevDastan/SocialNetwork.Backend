using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.User
{
    public class RegisterUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "The passwords are must be the same, try again.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
