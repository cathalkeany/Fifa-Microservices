using Microsoft.EntityFrameworkCore;
using PlayersService.Models;

namespace PlayersService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            
        }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Player> Players { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Club>()
                .HasMany(c => c.Players)
                .WithOne(c => c.Club!)
                .HasForeignKey(c => c.ClubId);

            modelBuilder
                .Entity<Player>()
                .HasOne(c => c.Club)
                .WithMany(c => c.Players)
                .HasForeignKey(c => c.ClubId);
        }
    }
}