using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodIdentifier.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingView : ContentView
    {
        public LoadingView()
        {
            InitializeComponent();
        }
    }
}