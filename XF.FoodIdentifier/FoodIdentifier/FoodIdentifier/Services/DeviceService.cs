using FoodIdentifier.Interfaces;
using Xamarin.Essentials;

namespace FoodIdentifier.Services
{
    public class DeviceService : IDeviceService
    {
        public string GetManufacturer()
        {
            return DeviceInfo.Manufacturer;
        }

        public string GetModel()
        {
            return DeviceInfo.Model;
        }

        public string GetPlatform()
        {
            return DeviceInfo.Platform.ToString();
        }

        public string GetVersion()
        {
            return DeviceInfo.VersionString;
        }
    }
}
