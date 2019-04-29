using System.Threading.Tasks;

namespace FoodIdentifier.Interfaces
{
    public interface IDialogService
    {
        Task OpenDialogAsync(string title, string message, string accept);
        Task CloseDialogAsync();
    }
}
