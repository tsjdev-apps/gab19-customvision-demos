using FoodIdentifier.Common;
using FoodIdentifier.Interfaces;
using FoodIdentifier.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FoodIdentifier.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private readonly IAppInfoService _appInfoService;
        private readonly IDeviceService _deviceService;
        private readonly ILauncherService _launcherService;
        private readonly IMailService _mailService;

        private string _appVersion;
        public string AppVersion
        {
            get => _appVersion;
            set => SetValue(ref _appVersion, value);
        }

        private string _appName;
        public string AppName
        {
            get => _appName;
            set => SetValue(ref _appName, value);
        }

        private AsyncCommand _initCommand;
        public AsyncCommand InitCommand => _initCommand ?? (_initCommand = new AsyncCommand(InitAsync));

        private AsyncCommand _sendMailCommand;
        public AsyncCommand SendMailCommand => _sendMailCommand ?? (_sendMailCommand = new AsyncCommand(SendMailAsync));

        private AsyncCommand _showTwitterCommand;
        public AsyncCommand ShowTwitterCommand => _showTwitterCommand ?? (_showTwitterCommand = new AsyncCommand(ShowTwitterAsync));

        private AsyncCommand _showWebsiteCommand;
        public AsyncCommand ShowWebsiteCommand => _showWebsiteCommand ?? (_showWebsiteCommand = new AsyncCommand(ShowWebsiteAsync));

        public AboutViewModel(IAppInfoService appInfoService, IDeviceService deviceService, ILauncherService launcherService, IMailService mailService)
        {
            _appInfoService = appInfoService;
            _deviceService = deviceService;
            _launcherService = launcherService;
            _mailService = mailService;
        }

        private async Task InitAsync()
        {
            IsLoading = true;

            await Task.Yield();

            AppName = _appInfoService.GetName();
            AppVersion = _appInfoService.GetVersion();

            IsLoading = false;
        }

        private async Task SendMailAsync()
        {
            try
            {
                await _mailService.SendMailAsync(new List<string> { AppResources.AboutViewPageToMail }, AppResources.AboutViewPageSubjectMail, CreateMailBody());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"AboutViewModel | SendMail | {ex.Message}");
            }
        }

        private async Task ShowTwitterAsync()
        {
            await _launcherService.OpenUriAsync(AppResources.AboutViewPageTwitterLink);
        }

        private async Task ShowWebsiteAsync()
        {
            await _launcherService.OpenUriAsync(AppResources.AboutViewPageWebsiteLink);
        }

        private string CreateMailBody()
        {
            // device info
            var deviceModel = _deviceService.GetModel();
            var manufacturer = _deviceService.GetManufacturer();
            var operatingSystemVersion = _deviceService.GetVersion();
            var platform = _deviceService.GetPlatform();

            // app info
            var applicationVersion = _appInfoService.GetVersion();
            var buildVersion = _appInfoService.GetBuild();

            return string.Format(AppResources.AboutViewPageBodyMail, deviceModel, manufacturer, operatingSystemVersion, platform, applicationVersion, buildVersion);
        }
    }
}
