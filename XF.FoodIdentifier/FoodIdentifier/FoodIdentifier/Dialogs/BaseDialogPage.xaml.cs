using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodIdentifier.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseDialogPage : PopupPage
    {
        public BaseDialogPage()
        {
            InitializeComponent();
        }

        public BaseDialogPage(View customView) : this()
        {
            DialogFrame.Content = customView;
            CloseWhenBackgroundIsClicked = false;
        }
    }
}