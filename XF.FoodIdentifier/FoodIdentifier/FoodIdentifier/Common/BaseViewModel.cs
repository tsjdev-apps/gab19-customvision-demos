using CommonServiceLocator;
using FoodIdentifier.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace FoodIdentifier.Common
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { SetValue(ref _isLoading, value); }
        }

        private ICommand _showAboutCommand;
        public ICommand ShowAboutCommand => _showAboutCommand ?? (_showAboutCommand = new Command(() => NavigationService.NavigateToAsync(Statics.AboutPage)));

        private ICommand _showSettingsCommand;
        public ICommand ShowSettingsCommand => _showSettingsCommand ?? (_showSettingsCommand = new Command(() => NavigationService.NavigateToAsync(Statics.SettingsPage)));


        public INavigationService NavigationService => ServiceLocator.Current.GetInstance<INavigationService>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return false;

            backingField = value;
            OnPropertyChanged(propertyName);

            return true;
        }
    }
}
