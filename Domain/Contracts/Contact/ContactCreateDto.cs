using DAL.Enum;
using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Contact;

public record ContactCreateDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; init; }

    // Surname, MiddleName и Email тоже могут быть обязательными
    public string? Surname { get; init; }

    public string? MiddleName { get; init; }

    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; init; }

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid phone number")]
    public string Phone { get; init; }

    [Required(ErrorMessage = "Status is required")]
    public ContactStatus Status { get; init; }
}
