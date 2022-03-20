using DigiAviator.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigiAviator.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Runway>()
                .HasIndex(r => r.AirportId)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<DigiAviator.Infrastructure.Data.Models.License> Licenses { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Medical> Medicals { get; set; }
        public DbSet<FitnessType> FitnessTypes { get; set; }
        public DbSet<Limitation> Limitations { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Runway> Runways { get; set; }

    }
}