using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {

        builder.HasOne(s => s.Lead)
           .WithMany(l => l.Sales)
           .HasForeignKey(s => s.LeadId);


        builder.HasOne(s => s.SalerUser)
            .WithMany(su => su.Sales)
            .HasForeignKey(s => s.SalerId)
            .OnDelete(DeleteBehavior.NoAction); ;
    }
}
