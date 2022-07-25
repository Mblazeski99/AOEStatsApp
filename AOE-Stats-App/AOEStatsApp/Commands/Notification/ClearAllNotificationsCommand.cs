using AOEStatsApp.Stores;
using AOEStatsApp.ViewModels;
using Domain.Enums;
using Domain.Models;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace AOEStatsApp.Commands
{
    public class ClearAllNotificationsCommand : AsyncCommandBase
    {
        private readonly NotificationsStore _notificationsStore;
        private readonly NotificationsLogViewModel _notificationsLogViewModel;

        public ClearAllNotificationsCommand(NotificationsStore notificationsStore, NotificationsLogViewModel notificationsLogViewModel)
        {
            _notificationsStore = notificationsStore;
            _notificationsLogViewModel = notificationsLogViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                MessageBoxResult answer = MessageBox.Show("Are you sure you want to clear the log?", "Clear Notifications", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (answer == MessageBoxResult.Yes)
                {
                    _notificationsLogViewModel.IsLoading = true;
                    await _notificationsStore.ClearNotifications();
                }
            }
            catch (Exception ex)
            {
                var error = new Notification("Failed to notifications", MessageType.Error);
                _notificationsStore.AddNotification(error, ex.ToString());
            }
            finally
            {
                _notificationsLogViewModel.IsLoading = false;
            }
        }
    }
}
