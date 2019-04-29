using FoodIdentifier.Common;
using FoodIdentifier.Interfaces;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FoodIdentifier.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ICustomVisionService _customVisionService;
        private readonly IDialogService _dialogService;
        private readonly IErrorService _errorService;
        private readonly IPhotoService _photoService;
        private readonly IResourceService _resourceService;

        private byte[] _imageBytes;
        public byte[] ImageBytes
        {
            get => _imageBytes;
            set { SetValue(ref _imageBytes, value); PredictPictureCommand.RaiseCanExecuteChange(); }
        }

        private AsyncCommand _takePictureCommand;
        public AsyncCommand TakePictureCommand => _takePictureCommand ?? (_takePictureCommand = new AsyncCommand(TakePictureAsync));

        private AsyncCommand _pickPictureCommand;
        public AsyncCommand PickPictureCommand => _pickPictureCommand ?? (_pickPictureCommand = new AsyncCommand(PickPictureAsync));

        private AsyncCommand _predictPictureCommand;
        public AsyncCommand PredictPictureCommand => _predictPictureCommand ?? (_predictPictureCommand = new AsyncCommand(PredictImageAsync, CanPredictImage));

        private bool CanPredictImage()
        {
            return ImageBytes != null;
        }

        public MainViewModel(ICustomVisionService customVisionService, IDialogService dialogService, IErrorService errorService, IPhotoService photoService, IResourceService resourceService)
        {
            _customVisionService = customVisionService;
            _dialogService = dialogService;
            _errorService = errorService;
            _photoService = photoService;
            _resourceService = resourceService;
        }

        private async Task TakePictureAsync()
        {
            try
            {
                ImageBytes = null;

                var bytes = await _photoService.TakePhotoAsync();
                if (bytes != null)
                    ImageBytes = bytes;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"MainViewModel | TakePictureAsync | {ex}");
                await _errorService.ShowErrorDialogAsync(ex);
            }
        }

        private async Task PickPictureAsync()
        {
            try
            {
                ImageBytes = null;

                var bytes = await _photoService.PickPhotoAsync();
                if (bytes != null)
                    ImageBytes = bytes;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"MainViewModel | PickPictureAsync | {ex}");
                await _errorService.ShowErrorDialogAsync(ex);
            }
        }

        private async Task PredictImageAsync()
        {
            IsLoading = true;

            try
            {
                var imagePredictionResult = await _customVisionService.PredictImageAsync(ImageBytes);

                if (imagePredictionResult != null)
                    await _dialogService.OpenDialogAsync(_resourceService.GetString("ResultDialogTitle"), GetPrediction(imagePredictionResult), _resourceService.GetString("DialogButtonOk"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"MainViewModel | PredictImageAsync | {ex}");
                await _errorService.ShowErrorDialogAsync(ex);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private string GetPrediction(ImagePrediction imagePrediction)
        {
            var prediction = imagePrediction.Predictions.OrderByDescending(x => x.Probability).FirstOrDefault();

            if (prediction.Probability < 0.5 || prediction.TagName.ToLower() == "negative")
                return _resourceService.GetString("ResultErrorMessage");

            return string.Format(_resourceService.GetString("ResultSuccessMessage"), (prediction.Probability * 100).ToString("F2"), prediction.TagName.ToUpper());
        }
    }
}
