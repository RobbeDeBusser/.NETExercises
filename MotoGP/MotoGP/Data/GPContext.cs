using Microsoft.EntityFrameworkCore;
using MotoGP.Models;

public class GPContext : DbContext
{
    public GPContext(DbContextOptions<GPContext> options) : base(options)
    {
    }

    public DbSet<Team> Teams { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Rider> Riders { get; set; }
    public DbSet<Race> Races { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>().ToTable("Team");
        modelBuilder.Entity<Country>().ToTable("Country");
        modelBuilder.Entity<Rider>().ToTable("Rider");
        modelBuilder.Entity<Race>().ToTable("Race");
        modelBuilder.Entity<Ticket>().ToTable("Ticket");

        modelBuilder.Entity<Rider>()
            .HasOne(r => r.Team)
            .WithMany(t => t.Riders)
            .HasForeignKey(r => r.TeamID);

        modelBuilder.Entity<Rider>()
            .HasOne(r => r.Country)
            .WithMany(c => c.Riders)
            .HasForeignKey(r => r.CountryID);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Country)
            .WithMany(c => c.Tickets)
            .HasForeignKey(t => t.CountryID);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Race)
            .WithMany(r => r.Tickets)
            .HasForeignKey(t => t.RaceID);
    }
}
