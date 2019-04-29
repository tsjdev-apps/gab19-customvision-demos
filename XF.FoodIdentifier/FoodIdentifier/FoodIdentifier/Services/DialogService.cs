using FoodIdentifier.Common;
using FoodIdentifier.Dialogs;
using FoodIdentifier.Interfaces;
using System.Threading.Tasks;

namespace FoodIdentifier.Services
{
    public class DialogService : IDialogService
    {
        private readonly INavigationService _navigationService;

        public DialogService(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async Task OpenDialogAsync(string title, string message, string accept)
        {
            await _navigationService.OpenPopupAsync(Statics.BaseDialogPage, new SimpleDialogView(title, message, accept, _navigationService));
        }

        public async Task CloseDialogAsync()
        {
            await _navigationService.ClosePopupAsync();
        }
    }
}
