using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TombStone2
{

    public sealed partial class MainPage : Page
    {
        DispatcherTimer dispatcherTimer;
        int timeoutSec = 3;

        public MainPage()
        {
            this.InitializeComponent();

            DispatcherTimerSetup();
        }

        public void DispatcherTimerSetup()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, timeoutSec);
            dispatcherTimer.Start();
        }

        void dispatcherTimer_Tick(object sender, object e)
        {
            dispatcherTimer.Stop();
            this.Frame.Navigate(typeof(HelloPage));
        }
    }
}
