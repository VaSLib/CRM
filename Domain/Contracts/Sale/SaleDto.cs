using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Sale;

public record SaleDto(
    int Id,
    int LeadId,
    int SalerId,
    DateTime DateOfSale
    );