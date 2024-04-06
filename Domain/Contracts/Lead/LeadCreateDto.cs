using DAL.Enum;

namespace Domain.Contracts.Lead;

public record LeadCreateDto(
    int ContactId,
    LeadStatus Status
    );
