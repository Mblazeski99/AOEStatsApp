using AOEStatsApp.Commands;
using AOEStatsApp.Stores;
using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AOEStatsApp.ViewModels
{
    public class NotificationsLogViewModel : ViewModelBase
    {
        private readonly NotificationsStore _notificationsStore;
        private readonly ObservableCollection<NotificationViewModel> _notifications;
        private bool _isItemsGridLoading;
        private bool _isLoading;

        public bool IsItemsGridLoading
        {
            get { return _isItemsGridLoading; }
            set
            {
                _isItemsGridLoading = value;
                OnPropertyChanged(nameof(IsItemsGridLoading));
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
                OnPropertyChanged(nameof(EnableInput));
            }
        }

        public bool EnableInput => !IsLoading;

        public IEnumerable<NotificationViewModel> Notifications => _notifications;

        public ICommand ClearNotificationsCommand { get; }

        public NotificationsLogViewModel(NotificationsStore notificationsStore)
        {
            _notificationsStore = notificationsStore;
            _notifications = new ObservableCollection<NotificationViewModel>();
            ClearNotificationsCommand = new ClearAllNotificationsCommand(notificationsStore, this);

            LoadNotifications();
            _notificationsStore.NotificationsChanged += OnNotificationsUpdated;
        }

        private async void LoadNotifications()
        {
            IsItemsGridLoading = true;

            try
            {
                await _notificationsStore.Load();
                _notifications.Clear();

                foreach (Notification notification in _notificationsStore.Notifications)
                {
                    _notifications.Add(new NotificationViewModel(notification));
                }
            }
            catch (Exception ex)
            {
                var error = new Notification("Failed to load notifications", MessageType.Error);
                _notificationsStore.AddNotification(error, ex.ToString());
            }
            finally
            {
                IsItemsGridLoading = false;
            }
        }

        private void OnNotificationsUpdated()
        {
            LoadNotifications();
        }

        public override void Dispose()
        {
            _notificationsStore.NotificationsChanged -= OnNotificationsUpdated;
            base.Dispose();
        }
    }
}
