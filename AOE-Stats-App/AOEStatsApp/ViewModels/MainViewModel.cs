using AOEStatsApp.Stores;

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

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
