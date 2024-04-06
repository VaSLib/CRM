using DAL.Enum;

namespace Domain.Contacts;

public record UserDto(int Id, string? FullName, string Email, Roles Roles);
