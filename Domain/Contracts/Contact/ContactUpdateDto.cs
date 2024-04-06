using DAL.Enum;

namespace Domain.Contracts.Contact;

public record ContactUpdateDto(
    int Id,
    string Name,
    string? Surname,
    string? MiddleName,
    string? Email,
    string Phone
    );
