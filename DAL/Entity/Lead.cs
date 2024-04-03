using DAL.Enum;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entity;

public class Lead
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public int ContactId { get; set; }
    public Contact? Contact { get; set; }

    public int SalerId { get; set; }
    public User? SalerUser { get; set; }

    [Required]
    public LeadStatus Status { get; set; }

    public List<Sale>? Sales { get; set; }


}
