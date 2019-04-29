using System;
using System.Threading.Tasks;

namespace FoodIdentifier.Interfaces
{
    public interface INavigationService
    {
        string CurrentPageKey { get; }


        void Initialize(object navigationPageInstance);
        void Configure(string key, Type type);
        Task GoBackAsync();
        Task NavigateToAsync(string key);
        Task NavigateToAsync(string key, object parameter);
        void ClearBackstack();
        Task OpenPopupAsync(string key);
        Task OpenPopupAsync(string key, object parameter);
        Task ClosePopupAsync();
    }
}
