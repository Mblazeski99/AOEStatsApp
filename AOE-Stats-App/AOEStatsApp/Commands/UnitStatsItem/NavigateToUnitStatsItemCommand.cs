using AOEStatsApp.Helpers;
using AOEStatsApp.Services;
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
        private readonly NavigationService<CreateOrEditUnitStatsItemViewModel> _navigationService;
        private readonly NotificationsStore _notificationsStore;
        private readonly UnitStatsItemListingViewModel _unitStatsItemListingViewModel;

        public NavigateToUnitStatsItemCommand(UnitStatsStore unitsStore, NavigationService<CreateOrEditUnitStatsItemViewModel> navigationService, NotificationsStore notificationsStore, UnitStatsItemListingViewModel unitStatsItemListingViewModel)
        {
            _unitsStore = unitsStore;
            _navigationService = navigationService;
            _notificationsStore = notificationsStore;
            _unitStatsItemListingViewModel = unitStatsItemListingViewModel;
        }

        public override void Execute(object parameter)
        {
            try
            {
                _unitStatsItemListingViewModel.IsLoading = true;

                var item = new UnitStatsItem();
                PropertyCopier<UnitStatsItemViewModel, UnitStatsItem>.Copy((parameter as UnitStatsItemViewModel), item);

                _unitsStore.SetCurrentUnitStatsItem(item);
                _navigationService.Navigate();
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
