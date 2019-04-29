using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;
using System.Threading.Tasks;

namespace FoodIdentifier.Interfaces
{
    public interface ICustomVisionService
    {
        Task<ImagePrediction> PredictImageAsync(byte[] imageStream);
    }
}
