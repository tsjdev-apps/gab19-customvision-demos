using System.Threading.Tasks;

namespace FoodIdentifier.Interfaces
{
    public interface IPhotoService
    {
        Task<byte[]> TakePhotoAsync();
        Task<byte[]> PickPhotoAsync();
    }
}
