using CommonServiceLocator;
using FoodIdentifier.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodIdentifier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsView : ContentPage
	{
        private readonly SettingsViewModel _viewModel;

        public SettingsView ()
		{
			InitializeComponent ();

            _viewModel = ServiceLocator.Current.GetInstance<SettingsViewModel>();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.InitCommand.Execute(null);
        }
    }
}