using System;
using System.Collections.Generic;
using System.Linq;
using TombStone2.Helpers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TombStone2
{
    public sealed partial class InstagramPage : Page
    {
        DispatcherTimer dispatcherTimer;
        int timeoutSec = 5;


        public InstagramPage()
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
            this.Frame.Navigate(typeof(CameraPage));
        }

        void PrepareContent()
        {
            var instagramPath = $"{Windows.ApplicationModel.Package.Current.InstalledLocation.Path}\\Instagrams";
            var items = new List<ControlInfoDataItem>();
            items.Add(new ControlInfoDataItem("Chillin out", "in da hood",
                $"{instagramPath}\\1.jpg"));
            items.Add(new ControlInfoDataItem("#Mighty dino", "don't eat me",
                $"{instagramPath}\\2.jpg"));
            items.Add(new ControlInfoDataItem("Totems", "it was groooovy",
                $"{instagramPath}\\3.jpg"));
            items.Add(new ControlInfoDataItem("Canada", "THE #leaf",
                $"{instagramPath}\\4.jpg"));
            items.Add(new ControlInfoDataItem("Mouse", "so tiny!",
                $"{instagramPath}\\5.jpg"));
            items.Add(new ControlInfoDataItem("Widgeon falls", "wet",
                $"{instagramPath}\\6.jpg"));
            items.Add(new ControlInfoDataItem("I thing he's dead.", "almost certain",
                $"{instagramPath}\\7.jpg"));
            items.Add(new ControlInfoDataItem("This is fine!", "Mmmmm ...",
                $"{instagramPath}\\8.jpg"));
            items.Add(new ControlInfoDataItem("Grass!", "up close",
                $"{instagramPath}\\9.jpg"));
            items.Add(new ControlInfoDataItem("Red bird", "it spells doom",
                $"{instagramPath}\\10.jpg"));


            var rand = items.OrderBy(arg => Guid.NewGuid()).Take(6).ToList();
            foreach (var controlInfoDataItem in rand)
            {
                InstagramPhotos?.Items?.Add(controlInfoDataItem);
            }
        }
    }
}
