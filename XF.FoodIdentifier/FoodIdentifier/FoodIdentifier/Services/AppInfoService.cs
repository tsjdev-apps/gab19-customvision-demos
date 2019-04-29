using FoodIdentifier.Interfaces;
using Xamarin.Essentials;

namespace FoodIdentifier.Services
{
    public class AppInfoService : IAppInfoService
    {
        public string GetBuild()
        {
            return AppInfo.BuildString;
        }

        public string GetName()
        {
            return AppInfo.Name;
        }

        public string GetVersion()
        {
            return AppInfo.VersionString;
        }
    }
}
