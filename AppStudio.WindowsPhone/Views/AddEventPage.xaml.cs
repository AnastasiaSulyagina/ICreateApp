using AppStudio.Services;
using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Storage.Streams;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace AppStudio.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddEventPage : Page
    {
        private DateTime EventDateTime;
        private BasicGeoposition geoposition = new BasicGeoposition();
        public Geopoint eventGeopoint;
        //public Decimal isMapVisible;
        public AddEventPage()
        {
            this.InitializeComponent();
            //isMapVisible = new Decimal(1);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            textFrame.Visibility = Visibility.Visible;
        }

        private void mapTapped(object sender, MapInputEventArgs e)
        {
            eventGeopoint = new Geopoint(e.Location.Position);

            //MapControl.SetLocation(PushPin, eventGeopoint);
        }
        
        private void showMap_Click(object sender, RoutedEventArgs e)
        {
            mapShow();

        }

        public void mapShow()
        {
            if (textFrame.Visibility == Visibility.Collapsed) textFrame.Visibility = Visibility.Visible;
            else textFrame.Visibility = Visibility.Collapsed;
            
        }
        private void setLocation_Click(object sender, RoutedEventArgs e)
        {
            textFrame.Visibility = Visibility.Visible;
        }

        private void newCommentBox(object sender, RoutedEventArgs e)
        {
            DateTimeButton.Flyout.Hide();
        }

        private void setDateTime_Click(object sender, RoutedEventArgs e)
        {
            DateTimeButton.Flyout.Hide();
        }
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            EventDateTime = new DateTime(2008, 5, 1, 8, 30, 52);
            if (!CurrentUser.isAuthorized)
            {
                NavigationServices.NavigateToPage("LoginPage");
            }
            else 
            {
                ServerAPI.AddEvent(DescriptionBox.Text, geoposition, EventDateTime);
            }
        }
    }
}
