using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Holiday>
    {
        public void Configure(EntityTypeBuilder<Holiday> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.HotelName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Stars).IsRequired();
            builder.Property(p => p.Description).IsRequired().HasMaxLength(180);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.DurationOfStay).IsRequired();
            builder.Property(p => p.City).IsRequired();
            builder.HasOne(m => m.MealPlan).WithMany().HasForeignKey(m => m.MealPlanId);
            builder.HasOne(t => t.TravelAgency).WithMany().HasForeignKey(t => t.TravelAgencyId);
            builder.HasOne(c => c.Country).WithMany().HasForeignKey(c => c.CountryId);
        }
    }
}