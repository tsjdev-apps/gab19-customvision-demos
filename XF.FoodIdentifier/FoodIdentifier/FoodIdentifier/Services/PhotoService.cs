using FoodIdentifier.Interfaces;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;
using System.Threading.Tasks;

namespace FoodIdentifier.Services
{
    public class PhotoService : IPhotoService
    {
        public async Task<byte[]> TakePhotoAsync()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                return null;

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { Directory = "hotdog", Name = "hotdog-test-img.jpg", DefaultCamera = CameraDevice.Rear, PhotoSize = PhotoSize.Small });

            if (file == null)
                return null;

            return GetBytesFromStream(file.GetStream());
        }

        public async Task<byte[]> PickPhotoAsync()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                return null;

            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { PhotoSize = PhotoSize.Small });

            if (file == null)
                return null;

            return GetBytesFromStream(file.GetStream());
        }

        private byte[] GetBytesFromStream(Stream input)
        {
            byte[] buffer = new byte[1024 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;

                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    ms.Write(buffer, 0, read);

                return ms.ToArray();
            }
        }
    }
}
