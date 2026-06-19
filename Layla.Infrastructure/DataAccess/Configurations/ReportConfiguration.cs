
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Layla.Domain.Entities;

namespace Layla.Infrastructure.DataAccess.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> entity)
        {
            entity.HasOne(r => r.Reporter)
                  .WithMany()
                  .HasForeignKey(r => r.ReporterId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.Apartment)
                  .WithMany()
                  .HasForeignKey(r => r.ApartmentId)
                  .OnDelete(DeleteBehavior.Cascade);

            
        }
    }
}
