using System.ComponentModel.DataAnnotations;

namespace DAL.Entity;

public class Sale
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public int LeadId { get; set; }

    [Required]
    public int SalerId { get; set; }

    [Required]
    public DateTime DateOfSale { get; set; }

    public Lead? Lead { get; set; }
    public User? SalerUser { get; set; }
}
