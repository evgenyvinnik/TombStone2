using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GridViewTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            var instagramPath = $"{Windows.ApplicationModel.Package.Current.InstalledLocation.Path}\\images\\Instagram";
            InstagramPhotos?.Items?.Add(new ControlInfoDataItem("Chillin out", "in da hood",
                $"{instagramPath}\\1.jpg"));
            InstagramPhotos?.Items?.Add(new ControlInfoDataItem("Game for first dibs on new desk.jpg", "spoiler - I got the crappy one",
                $"{instagramPath}\\2.jpg"));
            InstagramPhotos?.Items?.Add(new ControlInfoDataItem("Recovering after surgery.jpg", "it was groooovy",
                $"{instagramPath}\\3.jpg"));
            InstagramPhotos?.Items?.Add(new ControlInfoDataItem("Valentines day.jpg", "yum yum(in both senses of the word)",
                $"{instagramPath}\\4.jpg"));
            InstagramPhotos?.Items?.Add(new ControlInfoDataItem("Solar Eclipse.jpg", "I can see clearly now ...",
                $"{instagramPath}\\5.jpg"));
        }
    }
}