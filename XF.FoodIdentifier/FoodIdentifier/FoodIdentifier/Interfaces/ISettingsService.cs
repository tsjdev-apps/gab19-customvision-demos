namespace FoodIdentifier.Interfaces
{
    public interface ISettingsService
    {
        bool GetBool(string key, bool defaultValue);
        void SetBool(string key, bool value);
        string GetString(string key, string defaultValue);
        void SetString(string key, string value);
        bool ContainsKey(string key);
        void Remove(string key);
    }
 }
