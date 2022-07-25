using AOEStatsApp.Services.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AOEStatsApp.Stores
{
    public class NotificationsStore
    {
        private readonly INotificationService _notificationService;
        private readonly List<Notification> _notifications;
        private Lazy<Task> _initializeLazy;

        public IEnumerable<Notification> Notifications => _notifications;
        public event EventHandler? NotificationAdded;
        public event Action NotificationsChanged;

        public NotificationsStore(INotificationService notificationService)
        {
            _notificationService = notificationService;

            _notifications = new List<Notification>();
            _initializeLazy = new Lazy<Task>(Initialize);
        }

        public async void AddNotification(Notification notification, string? errorMessage = null)
        {
            notification.DateCreated = DateTime.Now;
            await _notificationService.CreateNotification(notification);
            NotificationAdded?.Invoke(notification, new EventArgs());
            OnNotificationsUpdated();

            if (!string.IsNullOrEmpty(errorMessage))
            {
                // TODO: Add notifications log in file.
            }
        }

        public async Task ClearNotifications()
        {
            await _notificationService.DeleteAllNotifications();
            OnNotificationsUpdated();
        }

        public async Task Load()
        {
            try
            {
                await _initializeLazy.Value;
            }
            catch (Exception ex)
            {
                _initializeLazy = new Lazy<Task>(Initialize);
                throw;
            }
        }

        private async Task OnNotificationsUpdated()
        {
            await Initialize();
            NotificationsChanged?.Invoke();
        }

        private async Task Initialize()
        {
            IEnumerable<Notification> items = await _notificationService.GetAllNotifications();

            _notifications.Clear();
            _notifications.AddRange(items);
        }
    }
}
