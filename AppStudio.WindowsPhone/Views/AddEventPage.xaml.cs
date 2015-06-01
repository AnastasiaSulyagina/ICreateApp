using AppStudio.Services;
using Common;
using System;
using System.Collections.ObjectModel;
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

namespace AppStudio.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddEventPage : Page
    {
        private DateTime EventDateTime = new DateTime();
        private BasicGeoposition geoposition = new BasicGeoposition();
        public Geopoint eventGeopoint;

        private ObservableCollection<Common.Event> eEvents = new ObservableCollection<Common.Event>();
        public ObservableCollection<Common.Event> Events
        {
            get { return eEvents; }
            set { eEvents = value; }
        }

        public AddEventPage()
        {
            this.InitializeComponent();

            EventDateTime = DateTime.Now;
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
            geoposition = e.Location.Position;
            eEvents.Clear();
            eEvents.Add((new Common.Event(1, "", new User(""), new DateTime(2008, 1, 1, 1, 1, 1), new DateTime(2008, 1, 1, 1, 1, 1), geoposition.Latitude, geoposition.Longitude, "")));
        }
        
        private void showMap_Click(object sender, RoutedEventArgs e)
        {
            mapShow();

        }
        private void HideError_Click(object sender, RoutedEventArgs e)
        {
            CreateButton.Flyout.Hide();
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


        private void setDateTime_Click(object sender, RoutedEventArgs e)
        {
            DateTimeButton.Flyout.Hide();
            var date = eventDatePicker.Date.DateTime;
            var time = eventTimePicker.Time;
            EventDateTime = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);
        }
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (!CurrentUser.isAuthorized)
            {
                NavigationServices.NavigateToPage("LoginPage");
            }
            else if (DescriptionBox.Text != "")
            {
                ErrorText.Text = "Событие создано";
                ServerAPI.AddEvent(DescriptionBox.Text, geoposition, EventDateTime);
                NavigationServices.NavigateToPage("MainPage");
            }
        }
    }
}
