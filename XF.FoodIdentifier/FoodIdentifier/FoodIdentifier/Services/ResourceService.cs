using FoodIdentifier.Interfaces;
using FoodIdentifier.Resources;

namespace FoodIdentifier.Services
{
    public class ResourceService : IResourceService
    {
        public string GetString(string key)
        {
            return AppResources.ResourceManager.GetString(key);
        }
    }
}
