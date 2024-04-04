using DAL.Enum;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entity;

public class Contact
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int MarketerId { get; set; }

    public User? MarketerUser { get; set; }

    [Required]
    public string Name { get; set; }

    public string? Surname { get; set; }

    public string? MiddleName { get; set; }

    public string? Email { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    public ContactStatus Status { get; set; }

    [Required]
    public DateTime Update { get; set; }

    public Lead? Lead { get; set; }
}
