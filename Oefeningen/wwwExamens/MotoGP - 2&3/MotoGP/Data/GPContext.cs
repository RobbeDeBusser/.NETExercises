using MotoGP.Models;
using Microsoft.EntityFrameworkCore;

namespace MotoGP.Data
{
    public class GPContext : DbContext
    {
        public GPContext(DbContextOptions<GPContext> options) : base(options)
        {
        }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Rider> Riders { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Race> Races { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>().ToTable("Ticket");
            modelBuilder.Entity<Rider>().ToTable("Rider");
            modelBuilder.Entity<Country>().ToTable("Country");
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<Race>().ToTable("Race");
        }
    }
}
