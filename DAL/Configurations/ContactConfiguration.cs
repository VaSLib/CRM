using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {

        builder.HasOne(c => c.MarketerUser)
            .WithMany(u => u.Contacts)
            .HasForeignKey(c => c.MarketerId)
            .HasPrincipalKey(u => u.Id);
    }
}
