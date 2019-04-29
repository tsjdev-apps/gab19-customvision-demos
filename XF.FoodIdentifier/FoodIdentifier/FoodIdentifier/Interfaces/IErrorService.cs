using System;
using System.Threading.Tasks;

namespace FoodIdentifier.Interfaces
{
    public interface IErrorService
    {
        Task ShowErrorDialogAsync(Exception ex);
    }
}
