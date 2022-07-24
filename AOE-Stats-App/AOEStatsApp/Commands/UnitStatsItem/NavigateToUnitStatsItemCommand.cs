using AOEStatsApp.Helpers;
using AOEStatsApp.Stores;
using AOEStatsApp.ViewModels;
using Domain.Enums;
using Domain.Models;
using System;

namespace AOEStatsApp.Commands
{
    public class NavigateToUnitStatsItemCommand : CommandBase
    {
        private readonly UnitStatsStore _unitsStore;
        private readonly NavigationStore _navigationStore;
        private readonly NotificationsStore _notificationsStore;
        private readonly UnitStatsItemListingViewModel _unitStatsItemListingViewModel;

        public NavigateToUnitStatsItemCommand(UnitStatsStore unitsStore, NotificationsStore notificationsStore, UnitStatsItemListingViewModel unitStatsItemListingViewModel, NavigationStore navigationStore)
        {
            _unitsStore = unitsStore;
            _notificationsStore = notificationsStore;
            _unitStatsItemListingViewModel = unitStatsItemListingViewModel;
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            try
            {
                _unitStatsItemListingViewModel.IsLoading = true;

                var item = new UnitStatsItem();
                PropertyCopier<UnitStatsItemViewModel, UnitStatsItem>.Copy((parameter as UnitStatsItemViewModel), item);

                _unitsStore.SetCurrentUnitStatsItem(item);
                _navigationStore.CurrentViewModel = new CreateOrEditUnitStatsItemViewModel(_navigationStore, _unitsStore, _notificationsStore);
            } 
            catch (Exception ex)
            {
                var error = new Notification("Failed to load item", MessageType.Error);
                _notificationsStore.AddNotification(error, ex.ToString());
            }
            finally
            {
                _unitStatsItemListingViewModel.IsLoading = false;
            }
        }
    }
}
