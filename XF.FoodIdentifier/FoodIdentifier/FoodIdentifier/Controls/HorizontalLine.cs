using Xamarin.Forms;

namespace FoodIdentifier.Controls
{
    public class HorizontalLine : BoxView
    {
        public HorizontalLine()
        {
            HeightRequest = 1;
            VerticalOptions = LayoutOptions.Center;
            HorizontalOptions = LayoutOptions.FillAndExpand;
        }
    }
}
