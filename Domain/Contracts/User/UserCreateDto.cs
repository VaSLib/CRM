using DAL.Enum;

namespace Domain.Contracts.User;

public record UserCreateDto(string? FullName, string Email, string Password, Roles Roles);