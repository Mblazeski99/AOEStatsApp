using AOEStatsApp.Stores;
using AOEStatsApp.ViewModels;
using Domain.Enums;
using Domain.Models;
using System;
using System.Threading.Tasks;

namespace AOEStatsApp.Commands
{
    public class LoadUnitsStatsItemsCommand : AsyncCommandBase
    {
        private readonly UnitStatsItemListingViewModel _viewModel;
        private readonly UnitStatsStore _unitsStore;
        private readonly NotificationsStore _notificationsStore;

        public LoadUnitsStatsItemsCommand(UnitStatsItemListingViewModel viewModel, UnitStatsStore unitsStore, NotificationsStore notificationsStore)
        {
            _viewModel = viewModel;
            _unitsStore = unitsStore;
            _notificationsStore = notificationsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsItemsGridLoading = true;

            try
            {
                await _unitsStore.Load();
                _viewModel.UpdateItems(_unitsStore.UnitStatsItems);
            }
            catch (Exception ex)
            {
                var error = new Notification("Failed to load items", MessageType.Error);
                _notificationsStore.AddNotification(error, ex.ToString());
            }
            finally
            {
                _viewModel.IsItemsGridLoading = false;
            }
        }
    }
}