using DAL.Enum;

namespace Domain.Contracts.Lead;

using System.ComponentModel.DataAnnotations;

public record LeadCreateDto
{
    [Required(ErrorMessage = "ContactId is required")]
    public int ContactId { get; init; }

    [Required(ErrorMessage = "Status is required")]
    public LeadStatus Status { get; init; }
}

