using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TombStone2
{
    public sealed partial class ThanksPage : Page
    {
        public ThanksPage()
        {
            this.InitializeComponent();
        }

        private void ThanksVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
