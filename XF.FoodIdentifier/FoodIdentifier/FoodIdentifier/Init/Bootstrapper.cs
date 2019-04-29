using CommonServiceLocator;
using FoodIdentifier.Common;
using FoodIdentifier.Dialogs;
using FoodIdentifier.Interfaces;
using FoodIdentifier.Services;
using FoodIdentifier.ViewModels;
using FoodIdentifier.Views;
using Unity;
using Unity.Lifetime;
using Unity.ServiceLocation;

namespace FoodIdentifier.Init
{
    public class Bootstrapper
    {
        public static void RegisterDependencies()
        {
            var container = new UnityContainer();

            // services - instances
            container.RegisterInstance(CreateNavigationService());

            // services
            container.RegisterType<IAppInfoService, AppInfoService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICustomVisionService, CustomVisionService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDeviceService, DeviceService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IErrorService, ErrorService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILauncherService, LauncherService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMailService, MailService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPhotoService, PhotoService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IResourceService, ResourceService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISettingsService, SettingsService>(new ContainerControlledLifetimeManager());

            // viewmodels
            container.RegisterType<AboutViewModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<MainViewModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<SettingsViewModel>(new ContainerControlledLifetimeManager());

            var locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }

        private static INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();

            navigationService.Configure(Statics.AboutPage, typeof(AboutView));
            navigationService.Configure(Statics.MainPage, typeof(MainView));
            navigationService.Configure(Statics.SettingsPage, typeof(SettingsView));
            navigationService.Configure(Statics.BaseDialogPage, typeof(BaseDialogPage));

            return navigationService;
        }
    }
}
