using FoodIdentifier.Interfaces;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FoodIdentifier.Services
{
    public class LauncherService : ILauncherService
    {
        public async Task OpenUriAsync(string uri)
        {
            await Browser.OpenAsync(uri, BrowserLaunchMode.External);
        }
    }
}
