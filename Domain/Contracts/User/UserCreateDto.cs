using DAL.Enum;

namespace Domain.Contracts.User;

using System.ComponentModel.DataAnnotations;

public record UserCreateDto
{
    [Required(ErrorMessage = "Full name is required")]
    public string? FullName { get; init; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; init; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    public string Password { get; init; }

    [Required(ErrorMessage = "User role is required")]
    public Roles Roles { get; init; }
}
