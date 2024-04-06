using DAL.Enum;

namespace Domain.Contracts.Lead;

public record LeadDto(
    int Id,
    int ContactId,
    int SalerId,
    LeadStatus Status
    );