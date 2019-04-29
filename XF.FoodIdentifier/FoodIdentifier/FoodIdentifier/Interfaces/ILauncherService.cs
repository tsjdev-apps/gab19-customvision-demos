using System.Threading.Tasks;

namespace FoodIdentifier.Interfaces
{
    public interface ILauncherService
    {
        Task OpenUriAsync(string uri);
    }
}
