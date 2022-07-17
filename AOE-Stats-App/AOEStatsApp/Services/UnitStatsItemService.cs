using AOEStatsApp.Services.Interfaces;
using DataContext;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AOEStatsApp.Services
{
    public class UnitStatsItemService : IUnitStatsItemService
    {
        private readonly AOEStatsDbContextFactory _dbContextFactory;

        public UnitStatsItemService(AOEStatsDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<UnitStatsItem>> GetAllItems()
        {
            using (AOEStatsDbContext context = _dbContextFactory.CreateDbContext())
            {
                return await context.UnitStatsItems.ToListAsync();
            }
        }

        public async Task CreateUnitStatsItem(UnitStatsItem item)
        {
            using (AOEStatsDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                dbContext.Add(item);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateUnitStatsItem(UnitStatsItem item)
        {
            using (AOEStatsDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                dbContext.Update(item);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteUnitStatsItem(UnitStatsItem item)
        {
            using (AOEStatsDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                dbContext.Remove(item);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
