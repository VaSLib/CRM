using DAL.Enum;

namespace Domain.Contracts.Contact;

public record ContactCreateDto(
    string Name,
    string? Surname,
    string? MiddleName,
    string? Email,
    string Phone,
    ContactStatus Status
    );
