using DAL.Enum;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entity;

public class User
{
    [Key]
    [Required]
    public int Id { get; set; }

    public string? FullName { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public Roles Roles { get; set; }

    public DateTime? DateOfBlocking { get; set; }

    public List<Contact>? Contacts { get; set; }
    public List<Lead>? Leads { get; set; }
    public List<Sale>? Sales { get; set; }
}
