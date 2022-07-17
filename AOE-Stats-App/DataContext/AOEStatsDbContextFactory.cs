using Microsoft.EntityFrameworkCore;

namespace DataContext
{
    public class AOEStatsDbContextFactory
    {
        private readonly string _connectionString;

        public AOEStatsDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public AOEStatsDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;

            return new AOEStatsDbContext(options);
        }
    }
}
