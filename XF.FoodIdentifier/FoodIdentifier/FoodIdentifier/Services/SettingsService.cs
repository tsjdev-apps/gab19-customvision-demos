using FoodIdentifier.Interfaces;
using Xamarin.Essentials;

namespace FoodIdentifier.Services
{
    public class SettingsService : ISettingsService
    {
        public bool ContainsKey(string key)
        {
            return Preferences.ContainsKey(key);
        }

        public bool GetBool(string key, bool defaultValue)
        {
            return Preferences.Get(key, defaultValue);
        }

        public string GetString(string key, string defaultValue)
        {
            return Preferences.Get(key, defaultValue);
        }

        public void Remove(string key)
        {
            Preferences.Remove(key);
        }

        public void SetBool(string key, bool value)
        {
            Preferences.Set(key, value);
        }

        public void SetString(string key, string value)
        {
            Preferences.Set(key, value);
        }
    }
}
