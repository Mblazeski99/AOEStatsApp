using AOEStatsApp.Stores;
using AOEStatsApp.ViewModels;
using Domain.Enums;
using Domain.Models;
using System;

namespace AOEStatsApp.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly UnitStatsStore _unitStatsStore;
        private readonly NavigationStore _navigationStore;
        private readonly NotificationsStore _notificationsStore;

        public NavigateCommand(NavigationStore navigationStore, UnitStatsStore unitStatsStore = null, NotificationsStore notificationsStore = null)
        {
            _unitStatsStore = unitStatsStore;
            _navigationStore = navigationStore;
            _notificationsStore = notificationsStore;
        }

        public override void Execute(object parameter)
        {
            try
            {
                if (parameter == null) throw new ArgumentNullException(nameof(parameter), "Parameter Was Null");

                string viewModelType = (parameter as dynamic).Name;
                ViewModelBase? viewModel = null;

                switch (viewModelType)
                {
                    case nameof(HomeViewModel):
                        if (_navigationStore.CurrentViewModel.GetType() != typeof(HomeViewModel))
                            viewModel = new HomeViewModel();
                        break;

                    case nameof(CreateOrEditUnitStatsItemViewModel):
                        if (_navigationStore.CurrentViewModel.GetType() != typeof(CreateOrEditUnitStatsItemViewModel))
                            viewModel = new CreateOrEditUnitStatsItemViewModel(_navigationStore, _unitStatsStore, _notificationsStore);
                        break;

                    case nameof(MainViewModel):
                        if (_navigationStore.CurrentViewModel.GetType() != typeof(MainViewModel))
                            viewModel = new MainViewModel(_unitStatsStore, _navigationStore, _notificationsStore);
                        break;

                    case nameof(NotificationsLogViewModel):
                        if (_navigationStore.CurrentViewModel.GetType() != typeof(NotificationsLogViewModel))
                            viewModel = new NotificationsLogViewModel(_notificationsStore);
                        break;

                    case nameof(UnitStatsItemListingViewModel):
                        if (_navigationStore.CurrentViewModel.GetType() != typeof(UnitStatsItemListingViewModel))
                            viewModel = new UnitStatsItemListingViewModel(_unitStatsStore, _notificationsStore, _navigationStore);
                        break;

                    default:
                        throw new Exception("ViewModel type does not exist");
                }

                if (viewModel != null)
                {
                    _navigationStore.CurrentViewModel = viewModel;
                }
            }
            catch (Exception ex)
            {
                Notification error = new Notification(ex.ToString(), MessageType.Error);
                _notificationsStore.AddNotification(error);
            }
        }
    }
}
