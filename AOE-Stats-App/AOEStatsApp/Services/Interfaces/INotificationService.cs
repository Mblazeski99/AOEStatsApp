using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AOEStatsApp.Services.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetAllNotifications();

        Task CreateNotification(Notification item);

        Task DeleteNotification(Notification item);

        Task DeleteAllNotifications();
    }
}
