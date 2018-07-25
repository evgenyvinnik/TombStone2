using System;
using System.Collections.Generic;
using System.Linq;
using TombStone2.Helpers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace TombStone2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TwitterPage : Page
    {
        DispatcherTimer dispatcherTimer;
        int timeoutSec = 5;

        public TwitterPage()
        {
            this.InitializeComponent();

            PrepareContent();

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
            this.Frame.Navigate(typeof(ThanksPage));
        }

        void PrepareContent()
        {
            var tweetsPath = $"{Windows.ApplicationModel.Package.Current.InstalledLocation.Path}\\Tweets";
            var items = new List<ControlInfoDataItem>();
            for(int i = 1; i <= 9; i++)
            {
                items.Add(new ControlInfoDataItem("", "",
                    $"{tweetsPath}\\{i}.png"));
            }
            items.Sort((x, y) => x.SortOrder.CompareTo(y.SortOrder));


            var rand = items.OrderBy(arg => Guid.NewGuid()).Take(1).ToList();
            foreach (var controlInfoDataItem in rand)
            {
                Tweets?.Items?.Add(controlInfoDataItem);
            }
        }
    }
}
