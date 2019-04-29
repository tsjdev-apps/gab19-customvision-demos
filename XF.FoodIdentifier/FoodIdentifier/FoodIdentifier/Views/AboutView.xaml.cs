using CommonServiceLocator;
using FoodIdentifier.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodIdentifier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutView : ContentPage
	{
        private readonly AboutViewModel _viewModel;

        public AboutView ()
		{
			InitializeComponent ();

            _viewModel = ServiceLocator.Current.GetInstance<AboutViewModel>();
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _viewModel.InitCommand.ExecuteAsync();
        }
    }
}