using FoodIdentifier.Interfaces;
using System;
using System.Threading.Tasks;

namespace FoodIdentifier.Services
{
    public class ErrorService : IErrorService
    {
        private readonly IDialogService _dialogService;
        private readonly IResourceService _resourceService;

        public ErrorService(IDialogService dialogService, IResourceService resourceService)
        {
            _dialogService = dialogService;
            _resourceService = resourceService;
        }

        public async Task ShowErrorDialogAsync(Exception ex)
        {
            await _dialogService.OpenDialogAsync(_resourceService.GetString("ErrorDialogTitle"),
                string.Format(_resourceService.GetString("ErrorDialogMessage"), ex.Message), 
                _resourceService.GetString("DialogButtonOk"));
        }
    }
}
