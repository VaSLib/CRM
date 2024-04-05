using DAL.Enum;

namespace Domain.Dto;

public record UserCreateDto (string? FullName, string Email, string Password,Roles Roles);