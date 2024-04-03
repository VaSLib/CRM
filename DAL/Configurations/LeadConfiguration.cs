using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class LeadConfiguration : IEntityTypeConfiguration<Lead>
{
    public void Configure(EntityTypeBuilder<Lead> builder)
    {
        builder.HasOne(l => l.Contact)
            .WithOne(c => c.Lead)
            .HasForeignKey<Lead>(l => l.ContactId)
            .HasPrincipalKey<Contact>(c => c.Id);

        builder.HasOne(l => l.SalerUser)
            .WithMany(s => s.Leads)
            .HasForeignKey(l => l.SalerId)
            .HasPrincipalKey(s => s.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
