using DAL.Enum;

namespace Domain.Contracts.Contact;

using System.ComponentModel.DataAnnotations;

public record ContactUpdateDto
{
    public int Id { get; init; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; init; }

    public string? Surname { get; init; }

    public string? MiddleName { get; init; }

    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; init; }

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid phone number")]
    public string Phone { get; init; }
}

