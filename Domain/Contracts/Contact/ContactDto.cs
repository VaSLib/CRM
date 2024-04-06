using DAL.Enum;

namespace Domain.Contracts.Contact;

public record ContactDto(
    int Id,
    int MarketerId,
    string Name,
    string? Surname,
    string? MiddleName,
    string? Email,
    string Phone,
    ContactStatus Status,
    DateTime Update
    );
