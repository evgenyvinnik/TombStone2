using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

using Windows.UI;

using Windows.UI.Xaml.Shapes;
using Windows.Devices.Input;

namespace TombStone2
{
    public sealed partial class HighFivePage : Page
    {
        int timesEnded = 1;
        int timesToEnd = 1;

        int intPointerCountUpdater = 0;
        int intPointerCountUpdaterFrequency = 20;

        public HighFivePage()
        {
            this.InitializeComponent();
        }

        private SortedSet<uint> setId = new SortedSet<uint>();

        private void GridPad_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            IList<Windows.UI.Input.PointerPoint> IlistPointer = e.GetIntermediatePoints(GridPad);
            int intPointerCount = IlistPointer.Count();

            byte[] rgb = new byte[3];
            (new Random()).NextBytes(rgb);
            Color color = Color.FromArgb(255, rgb[0], rgb[1], rgb[2]);

            var blnIsMouse = e.Pointer.PointerDeviceType == PointerDeviceType.Mouse;

            if (blnIsMouse)
                return;

            if (intPointerCountUpdater % intPointerCountUpdaterFrequency == 0)
            {
                PointsCountTextBlock.Text = intPointerCount.ToString();
                intPointerCountUpdater = 0;
            }
            else
            {
                intPointerCountUpdater++;
            }

            //Pointer saved in reversed mode ...
            for (int i = intPointerCount - 1; i >= 0; i--)
            {
                Windows.UI.Input.PointerPoint pointer = IlistPointer[i];

                // User PointerId for identify sequence (line) - if needed
                setId.Add(pointer.PointerId); // Add to Set - Set Automatically does not include duplicate Id ... 

                Point point = pointer.Position;

                // Prevent adding ellipse if mouse over grid and not left pressed ...
                if (blnIsMouse && !pointer.Properties.IsLeftButtonPressed) continue;

                //Properties -  https://msdn.microsoft.com/en-us/library/windows.ui.input.pointerpointproperties.aspx
                //if your devise has stylus ...
                float pressure = pointer.Properties.Pressure;

                // 48 just randomly chosen value...
                // value pressure always 0.5 if not pen (stylus) ...


                // Pay attention about simulator - pressure will be very small 
                //pressure = 1; // use for simulate

                double w = 48.0 * pressure;
                double h = 48.0 * pressure;

                if (point.X < w / 2.0 || point.X > GridPad.ActualWidth - w / 2)
                {
                    continue;  // add ellipse only on grid
                }

                if (point.Y < h / 2.0 || point.Y > GridPad.ActualHeight - h / 2)
                {
                    continue; // add ellipse only on grid
                }

                var tr = new TranslateTransform();
                tr.X = point.X - GridPad.ActualWidth / 2.0;
                tr.Y = point.Y - GridPad.ActualHeight / 2.0;

                Ellipse el = new Ellipse()
                {
                    Width = w,
                    Height = h,
                    Fill = new SolidColorBrush(color),
                    RenderTransform = tr,
                    Visibility = Visibility.Visible
                };

                GridPad.Children.Add(el);
            }

            // base.OnPointerMoved(e); //You can see differences if uncomment this line
        }

        private void HighFiveVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            timesEnded++;

            if (timesEnded > timesToEnd)
            {
                this.Frame.Navigate(typeof(InstagramPage));
            }
            else
            {
                HighFiveVideo.Play();
            }
        }
    }
}
