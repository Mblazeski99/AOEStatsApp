using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataContext
{
    public class AOEStatsDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AOEStatsDbContext>
    {
        public AOEStatsDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder().UseSqlite("Data Source=aoestats.db").Options;

            return new AOEStatsDbContext(options);
        }
    }
}
