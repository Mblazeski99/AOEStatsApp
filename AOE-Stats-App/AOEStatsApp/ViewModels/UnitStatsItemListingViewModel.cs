using AOEStatsApp.Commands;
using AOEStatsApp.Stores;
using Domain.Models;
using AOEStatsApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AOEStatsApp.ViewModels
{
    public class UnitStatsItemListingViewModel : ViewModelBase
    {
        private readonly UnitStatsStore _unitsStore;
        private readonly NotificationsStore _notificationsStore;
        private readonly ObservableCollection<UnitStatsItemViewModel> _unitStatsItems;
        private bool _isItemsGridLoading;
        private bool _isLoading;

        public IEnumerable<UnitStatsItemViewModel> UnitStatsItems => _unitStatsItems;

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

        public ICommand LoadUnitStatsItemCommand { get; }
        public ICommand CreateNewItemCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand DeleteItemCommand { get; }

        public UnitStatsItemListingViewModel(UnitStatsStore unitsStore, NavigationService<CreateOrEditUnitStatsItemViewModel> navigationService, NotificationsStore notificationsStore)
        {
            _unitsStore = unitsStore;
            _notificationsStore = notificationsStore;

            _unitStatsItems = new ObservableCollection<UnitStatsItemViewModel>();
            LoadUnitStatsItemCommand = new LoadUnitsStatsItemsCommand(this, _unitsStore, _notificationsStore);
            CreateNewItemCommand = new NavigateCommand<CreateOrEditUnitStatsItemViewModel>(navigationService);
            EditItemCommand = new NavigateToUnitStatsItemCommand(unitsStore, navigationService, notificationsStore, this);
            DeleteItemCommand = new DeleteUnitStatsItemCommand(notificationsStore, unitsStore, this);

            _unitsStore.SetCurrentUnitStatsItem(null as UnitStatsItem);
            _unitsStore.ItemsUpdated += OnUnitStatsItemsUpdated;
        }

        public override void Dispose()
        {
            _unitsStore.ItemsUpdated -= OnUnitStatsItemsUpdated;
            base.Dispose();
        }

        private void OnUnitStatsItemsUpdated()
        {
            LoadUnitStatsItemCommand.Execute(null);
        }

        public static UnitStatsItemListingViewModel LoadViewModel(UnitStatsStore unitsStore, NavigationService<CreateOrEditUnitStatsItemViewModel> navigationService, NotificationsStore notificationsStore)
        {
            var viewModel = new UnitStatsItemListingViewModel(unitsStore, navigationService, notificationsStore);
            viewModel.LoadUnitStatsItemCommand.Execute(null);
            return viewModel;
        }

        public void UpdateItems(IEnumerable<UnitStatsItem> items)
        {
            _unitStatsItems.Clear();

            foreach (var item in items)
            {
                _unitStatsItems.Add(new UnitStatsItemViewModel(item));
            }
        }
    }
}
