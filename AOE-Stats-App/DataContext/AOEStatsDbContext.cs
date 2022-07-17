using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataContext
{
    public class AOEStatsDbContext : DbContext
    {
        public AOEStatsDbContext(DbContextOptions options) : base(options) { }

        public DbSet<UnitStatsItem> UnitStatsItems { get; set; }
    }
}
