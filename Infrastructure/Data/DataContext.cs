using System.Reflection;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Offer> Offers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<MealPlan> MealPlans { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<TravelAgency> TravelAgencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}