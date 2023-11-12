using ClubService.Models;
using Microsoft.EntityFrameworkCore;

namespace ClubService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            
        }

        public DbSet<Club> Clubs { get; set; }
    }
}