using AOEStatsApp.Services.Interfaces;
using DataContext;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AOEStatsApp.Services
{
    public class NotificationService : INotificationService
    {
        private readonly AOEStatsDbContextFactory _dbContextFactory;

        public NotificationService(AOEStatsDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Notification>> GetAllNotifications()
        {
            using (AOEStatsDbContext context = _dbContextFactory.CreateDbContext())
            {
                return await context.Notifications.ToListAsync();
            }
        }

        public async Task CreateNotification(Notification item)
        {
            using (AOEStatsDbContext context = _dbContextFactory.CreateDbContext())
            {
                context.Add(item);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteNotification(Notification item)
        {
            using (AOEStatsDbContext context = _dbContextFactory.CreateDbContext())
            {
                context.Remove(item);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAllNotifications()
        {
            using (AOEStatsDbContext context = _dbContextFactory.CreateDbContext())
            {
                var itemsToRemove = await context.Notifications.ToListAsync();

                context.RemoveRange(itemsToRemove);
                await context.SaveChangesAsync();
            }
        }
    }
}
