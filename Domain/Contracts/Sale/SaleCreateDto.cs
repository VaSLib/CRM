namespace Domain.Contracts.Sale;

using System.ComponentModel.DataAnnotations;

public record SaleCreateDto
{
    [Required(ErrorMessage = "LeadId is required")]
    public int LeadId { get; init; }

    [Required(ErrorMessage = "SalerId is required")]
    public int SalerId { get; init; }
}
