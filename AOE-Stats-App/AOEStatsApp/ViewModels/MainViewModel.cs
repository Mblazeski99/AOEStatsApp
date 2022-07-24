using AOEStatsApp.Commands;
using AOEStatsApp.Stores;
using System.Windows.Input;

namespace AOEStatsApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private bool _isNavMenuOpen = false;

        public bool IsNavMenuOpen 
        {
            get { return _isNavMenuOpen; }
            set
            {
                _isNavMenuOpen = value;
                OnPropertyChanged(nameof(IsNavMenuOpen));
            }
        }

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public ICommand NavigateCommand { get; }

        public MainViewModel(UnitStatsStore unitStatsStore, NavigationStore navigationStore, NotificationsStore notificationsStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NavigateCommand = new NavigateCommand(unitStatsStore: unitStatsStore, navigationStore: navigationStore, notificationsStore: notificationsStore);
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
