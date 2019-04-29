using FoodIdentifier.Interfaces;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodIdentifier.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SimpleDialogView : ContentView
	{
        private readonly INavigationService _navigationService;

        public SimpleDialogView(string title, string message, string accept, INavigationService navigationService)
        {
            InitializeComponent();

            _navigationService = navigationService;

            DialogTitleLabel.Text = title;
            DialogMessageLabel.Text = message;
            DialogAcceptLabel.Text = accept;

            // Register tapped event on DialogAcceptLabel
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += TapGestureRecognizerOnTapped;
            DialogAcceptLabel.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private async void TapGestureRecognizerOnTapped(object sender, EventArgs e)
        {
            await _navigationService.ClosePopupAsync();
        }
    }
}