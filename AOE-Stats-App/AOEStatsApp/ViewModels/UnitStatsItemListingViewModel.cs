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

        public ICommand EditItemCommand { get; }
        public ICommand NavigateCommand { get; }
        public ICommand DeleteItemCommand { get; }

        public UnitStatsItemListingViewModel(UnitStatsStore unitsStore, NotificationsStore notificationsStore, NavigationStore navigationStore)
        {
            _unitsStore = unitsStore;
            _notificationsStore = notificationsStore;

            _unitStatsItems = new ObservableCollection<UnitStatsItemViewModel>();
            NavigateCommand = new NavigateCommand(navigationStore, unitStatsStore: unitsStore, notificationsStore: notificationsStore);
            EditItemCommand = new NavigateToUnitStatsItemCommand(unitsStore, notificationsStore, this, navigationStore);
            DeleteItemCommand = new DeleteUnitStatsItemCommand(notificationsStore, unitsStore, this);

            LoadUnitStatsItems();

            _unitsStore.SetCurrentUnitStatsItem(null as UnitStatsItem);
            _unitsStore.ItemsUpdated += OnUnitStatsItemsUpdated;
        }

        private async void LoadUnitStatsItems()
        {
            IsItemsGridLoading = true;

            try
            {
                await _unitsStore.Load();
                _unitStatsItems.Clear();

                foreach (var item in _unitsStore.UnitStatsItems)
                {
                    _unitStatsItems.Add(new UnitStatsItemViewModel(item));
                }
            }
            catch (Exception ex)
            {
                var error = new Notification("Failed to load items", MessageType.Error);
                _notificationsStore.AddNotification(error, ex.ToString());
            }
            finally
            {
                IsItemsGridLoading = false;
            }
        }

        public override void Dispose()
        {
            _unitsStore.ItemsUpdated -= OnUnitStatsItemsUpdated;
            base.Dispose();
        }

        private void OnUnitStatsItemsUpdated()
        {
            LoadUnitStatsItems();
        }
    }
}
