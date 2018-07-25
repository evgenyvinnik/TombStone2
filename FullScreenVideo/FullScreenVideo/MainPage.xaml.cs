using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FullScreenVideo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void PersonVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            ContentDialogExample dialog = new ContentDialogExample();
            var result = await dialog.ShowAsync();

            //if (result == ContentDialogResult.Primary)
            //{
            //    DialogResult.Text = "User saved their work";
            //}
            //else if (result == ContentDialogResult.Secondary)
            //{
            //    DialogResult.Text = "User did not save their work";
            //}
            //else
            //{
            //    DialogResult.Text = "User cancelled the dialog";
            //}
        }


    }
}
