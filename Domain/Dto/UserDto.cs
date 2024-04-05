using DAL.Enum;

namespace Domain.Dto;

public record UserDto(int Id, string? FullName, string Email, Roles Roles);
