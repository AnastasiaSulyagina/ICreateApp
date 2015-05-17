using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;

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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using AppStudio.Services;
using AppStudio.ViewModels;

namespace AppStudio.Views
{
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Common.Event> eEvents = new ObservableCollection<Common.Event>();
        public ObservableCollection<Common.Event> Events
        {
            get { return eEvents; }
            set { eEvents = value; }
        }

        public Geopoint MapCenter;
        private BasicGeoposition currentGeo = new BasicGeoposition();

        private Geolocator geolocator = new Geolocator();
            


        public MainPage()
        {
            this.InitializeComponent();
            update();


        }
        

        private async void update()
        {
            
            geolocator.DesiredAccuracyInMeters = 50;
            var cts = new CancellationTokenSource();
            //this.updateProgressBar.IsIndeterminate = true;
            var lastString = "";
            do
            {
                string JsnString;
                try
                {
                    JsnString = await Common.ServerAPI.GetEvents();
                    Geoposition geoposition = await geolocator.GetGeopositionAsync(
                        maximumAge: TimeSpan.FromMinutes(5),
                        timeout: TimeSpan.FromSeconds(10)
                    );
                    currentGeo.Latitude = geoposition.Coordinate.Latitude;
                    currentGeo.Longitude = geoposition.Coordinate.Longitude;
                    MapCenter = new Geopoint(currentGeo);
                }
                catch
                {
                    continue;
                }

                var deser = JsonConvert.DeserializeObject<ObservableCollection<Common.Event>>(JsnString);

                if (!lastString.Equals(JsnString))
                {
                    eEvents.Clear();
                    foreach(var elem in deser)
                    {
                        eEvents.Add(new Common.Event(elem.EventId, elem.LocationCaption, new Common.User(elem.User.UserName, elem.User.UserId, elem.User.Photo), elem.EventDate, elem.DateCreate, elem.Latitude, elem.Longitude, elem.Description));
                    }
                }
                await loop(cts.Token);
                
                //this.updateProgressBar.IsIndeterminate = false;
                cts.Cancel();
                lastString = JsnString;
            } while (true);
        }

        private async Task<int> loop(CancellationToken ct)
        {


            await Task.Delay(1000);
            return 1;
        }

        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.
        /// Этот параметр обычно используется для настройки страницы.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        private void Pushpin_Tap(object sender, RoutedEventArgs e)
        {
            Common.Event elem = (Common.Event)((Image)e.OriginalSource).DataContext;
            if (elem.misVisible == Visibility.Visible) elem.misVisible = Visibility.Collapsed;
            else elem.misVisible = Visibility.Visible;
            var index = eEvents.IndexOf(elem);
            eEvents.Remove(elem);
            eEvents.Insert(index, elem);
            
            /*
            ObservableCollection<DependencyObject> children = MapExtensions.GetChildren(map);

            foreach (DependencyObject obj in children)
            {
                Pushpin pin;
                try
                {
                    pin = (Pushpin)obj;
                }
                catch (Exception eeee)
                {
                    continue;
                }
                if (pin.Content == null)
                {
                    pin.Content = "";
                }
                if (pin != null)
                {
                    string s = pin.Content as String;
                    if (s != "")
                    {
                        pin.Content = "";
                    }
                }
            }
            //((Pushpin)sender).Content = (((Pushpin)sender).Tag as Event).description;
            ((Pushpin)sender).Content = (((Pushpin)sender).Tag as Event).MySquareDescriprion;

            tapFlag = true;
            //pin.Content = (pin.Tag as Event).description;
            // MessageBox.Show((pin.Tag as Event).description);
          */
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (Common.CurrentUser.isAuthorized)
            {
                NavigationServices.NavigateToPage("AddEventPage");
            }
            else
            {
                NavigationServices.NavigateToPage("LoginPage");
            }
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationServices.NavigateToPage("LoginPage");
        }

        private void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationServices.NavigateToPage("PicturePage");
        }

        private void eventList_ItemClick(object sender, ItemClickEventArgs e)
        {
            NavigationServices.NavigateToPage("EventPage", e.ClickedItem);
        }
        
    }
}
