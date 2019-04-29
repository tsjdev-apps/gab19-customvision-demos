using LocalFoodIdentifier.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace LocalFoodIdentifier
{
    public sealed partial class MainPage : Page
    {
        private Stopwatch _stopwatch = new Stopwatch();
        private OnnxModel _model = null;
		
		// put your ONNX file in the Assets folder
        private readonly string _ourOnnxFileName = "NAME OF YOUR ONNX FILE";

        public MainPage()
        {
            InitializeComponent();
        }

        private async Task AddTextToStatusBlock(string text)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => StatusBlock.Text += $"\r\n\r\n{text}");
        }

        private async Task LoadModelAsync()
        {
            await AddTextToStatusBlock($"Loading {_ourOnnxFileName} ... patience ");

            try
            {
                _stopwatch = Stopwatch.StartNew();

                var modelFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri($"ms-appx:///Assets/{_ourOnnxFileName}"));
                _model = await OnnxModel.CreateOnnxModel(modelFile);

                _stopwatch.Stop();

                await AddTextToStatusBlock($"Loaded {_ourOnnxFileName}: Elapsed time: {_stopwatch.ElapsedMilliseconds} ms");
            }
            catch (Exception ex)
            {
                await AddTextToStatusBlock($"LoadModelAsync | Error: {ex.Message}");
                _model = null;
            }
        }

        private async Task EvaluateVideoFrameAsync(VideoFrame frame)
        {
            if (frame != null)
            {
                try
                {
                    _stopwatch.Restart();

                    OnnxModelInput inputData = new OnnxModelInput { Data = frame };

                    var output = await _model.EvaluateAsync(inputData);

                    var tagName = output.ClassLabel.GetAsVectorView()[0];
                    var tagProbability = output.Loss[0][tagName];

                    _stopwatch.Stop();

                    var predictionString = string.Join(",  ", tagName + " " + (tagProbability * 100.0f).ToString("#0.00") + "%");

                    await AddTextToStatusBlock($"Evaluation took {_stopwatch.ElapsedMilliseconds}ms to execute, Predictions: {predictionString}.");
                }
                catch (Exception ex)
                {
                    await AddTextToStatusBlock($"EvaluateVideoFrameAsync | Error: {ex.Message}");
                }
            }
        }

        private async void StartButtonOnClick(object sender, RoutedEventArgs e)
        {

            StartButton.IsEnabled = false;
            UIPreviewImage.Source = null;

            try
            {
                if (_model == null)
                {
                    // Load the model
                    await LoadModelAsync();
                }

                // Trigger file picker to select an image file
                var fileOpenPicker = new FileOpenPicker { SuggestedStartLocation = PickerLocationId.PicturesLibrary, ViewMode = PickerViewMode.Thumbnail };
                fileOpenPicker.FileTypeFilter.Add(".bmp");
                fileOpenPicker.FileTypeFilter.Add(".jpg");
                fileOpenPicker.FileTypeFilter.Add(".png");
                fileOpenPicker.FileTypeFilter.Add(".jpeg");
                fileOpenPicker.FileTypeFilter.Add(".gif");

                var selectedStorageFile = await fileOpenPicker.PickSingleFileAsync();

                if (selectedStorageFile == null)
                    return;

                SoftwareBitmap softwareBitmap;
                using (var stream = await selectedStorageFile.OpenAsync(FileAccessMode.Read))
                {
                    // Create the decoder from the stream 
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);

                    // Get the SoftwareBitmap representation of the file in BGRA8 format
                    softwareBitmap = await decoder.GetSoftwareBitmapAsync();
                    softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
                }

                // Display the image
                var imageSource = new SoftwareBitmapSource();
                await imageSource.SetBitmapAsync(softwareBitmap);

                UIPreviewImage.Source = imageSource;
                
                // Encapsulate the image in the WinML image type (VideoFrame) to be bound and evaluated
                VideoFrame inputImage = VideoFrame.CreateWithSoftwareBitmap(softwareBitmap);
                
                // Evaluate the image
                await EvaluateVideoFrameAsync(inputImage);
            }
            catch (Exception ex)
            {
                await AddTextToStatusBlock($"StartButtonOnClick | Error: {ex.Message}");
            }
            finally
            {
                StartButton.IsEnabled = true;
            }
        }
    }
}
