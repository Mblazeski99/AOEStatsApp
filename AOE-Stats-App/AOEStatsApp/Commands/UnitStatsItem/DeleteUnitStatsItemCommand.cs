using AOEStatsApp.Helpers;
using AOEStatsApp.Stores;
using AOEStatsApp.ViewModels;
using Domain.Enums;
using Domain.Models;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace AOEStatsApp.Commands
{
    public class DeleteUnitStatsItemCommand : AsyncCommandBase
    {
        private readonly UnitStatsStore _unitStatsStore;
        private readonly NotificationsStore _notificationsStore;
        private readonly UnitStatsItemListingViewModel _unitStatsItemListingViewModel;

        public DeleteUnitStatsItemCommand(NotificationsStore notificationsStore, UnitStatsStore unitStatsStore, UnitStatsItemListingViewModel unitStatsItemListingViewModel)
        {
            _notificationsStore = notificationsStore;
            _unitStatsStore = unitStatsStore;
            _unitStatsItemListingViewModel = unitStatsItemListingViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                MessageBoxResult answer = MessageBox.Show("Are you sure you want to delete this unit", "Delete Unit", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (answer == MessageBoxResult.Yes)
                {
                    _unitStatsItemListingViewModel.IsLoading = true;

                    var item = new UnitStatsItem();
                    PropertyCopier<UnitStatsItemViewModel, UnitStatsItem>.Copy((parameter as UnitStatsItemViewModel), item);

                    await _unitStatsStore.DeleteUnitStatsItem(item);
                    var notification = new Notification("Successfully deleted item", MessageType.Success);
                    _notificationsStore.AddNotification(notification);
                }
            }
            catch (Exception ex)
            {
                var error = new Notification("Failed to delete item", MessageType.Error);
                _notificationsStore.AddNotification(error, ex.ToString());
            }
            finally
            {
                _unitStatsItemListingViewModel.IsLoading = false;
            }
        }
    }
}
