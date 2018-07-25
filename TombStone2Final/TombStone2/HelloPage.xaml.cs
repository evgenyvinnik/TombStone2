using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TombStone2
{
    public sealed partial class HelloPage : Page
    {
        public HelloPage()
        {
            this.InitializeComponent();
        }

        private void HelloVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HighFivePage));
        }
    }
}
