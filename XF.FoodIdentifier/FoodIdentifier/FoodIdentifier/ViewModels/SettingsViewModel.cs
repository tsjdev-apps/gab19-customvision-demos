using FoodIdentifier.Common;
using FoodIdentifier.Interfaces;
using System.Windows.Input;
using Xamarin.Forms;

namespace FoodIdentifier.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly ISettingsService _settingsService;

        private string _predictionKey;
        public string PredictionKey
        {
            get => _predictionKey;
            set => SetValue(ref _predictionKey, value);
        }

        private string _endpoint;
        public string Endpoint
        {
            get => _endpoint;
            set => SetValue(ref _endpoint, value);
        }

        private string _projectId;
        public string ProjectId
        {
            get => _projectId;
            set => SetValue(ref _projectId, value);
        }

        private string _iterationName;
        public string IterationName
        {
            get => _iterationName;
            set => SetValue(ref _iterationName, value);
        }

        private ICommand _initCommand;
        public ICommand InitCommand => _initCommand ?? (_initCommand = new Command(Init));

        private ICommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new Command(Save));

        public SettingsViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        private void Init()
        {
            PredictionKey = _settingsService.GetString(Statics.SettingsPredictionKey, Statics.SettingsPredictionKeyDefault);
            Endpoint = _settingsService.GetString(Statics.SettingsEndpoint, Statics.SettingsEndpointDefault);
            ProjectId = _settingsService.GetString(Statics.SettingsProjectId, Statics.SettingsProjectIdDefault);
            IterationName = _settingsService.GetString(Statics.SettingsIterationName, Statics.SettingsIterationNameDefault);
        }

        private async void Save()
        {
            _settingsService.SetString(Statics.SettingsPredictionKey, PredictionKey);
            _settingsService.SetString(Statics.SettingsEndpoint, Endpoint);
            _settingsService.SetString(Statics.SettingsProjectId, ProjectId);
            _settingsService.SetString(Statics.SettingsIterationName, IterationName);

            await NavigationService.GoBackAsync();
        }
    }
}
