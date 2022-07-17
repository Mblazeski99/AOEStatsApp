using Domain.Models;
using System;
using System.Collections.Generic;

namespace AOEStatsApp.Stores
{
    public class NotificationsStore
    {
        public List<Notification> Notifications { get; private set; }
        public event EventHandler? NotificationsChanged;

        public NotificationsStore()
        {
            Notifications = new List<Notification>();
        }

        public void AddNotification(Notification notification, string? errorMessage = null)
        {
            Notifications.Add(notification);
            NotificationsChanged?.Invoke(notification, new EventArgs());

            if (!string.IsNullOrEmpty(errorMessage))
            {
                // TODO: Add notifications log in file.
            }
        }
    }
}
