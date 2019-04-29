using FoodIdentifier.Common;
using FoodIdentifier.Interfaces;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace FoodIdentifier.Services
{
    public class CustomVisionService : ICustomVisionService
    {
        private readonly ISettingsService _settingsService;

        public CustomVisionService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public async Task<ImagePrediction> PredictImageAsync(byte[] imageBytes)
        {
            try
            {
                var predictionKey = _settingsService.GetString(Statics.SettingsPredictionKey, Statics.SettingsPredictionKeyDefault);
                var endpoint = _settingsService.GetString(Statics.SettingsEndpoint, Statics.SettingsEndpointDefault);
                var projectId = _settingsService.GetString(Statics.SettingsProjectId, Statics.SettingsProjectIdDefault);
                var iterationName = _settingsService.GetString(Statics.SettingsIterationName, Statics.SettingsIterationNameDefault);

                var predictionClient = new CustomVisionPredictionClient()
                {
                    ApiKey = predictionKey,
                    Endpoint = string.Format(Statics.EndpointUrl, endpoint)
                };

                return await predictionClient.ClassifyImageAsync(Guid.Parse(projectId), iterationName, new MemoryStream(imageBytes));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CustomVisionService | PredictImageAsync | {ex}");
                return null;
            }
        }
    }
}
