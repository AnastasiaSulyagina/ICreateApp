using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Background;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Common;
using AppStudio.Services;
using AppStudio.ViewModels;

namespace AppStudio.Views
{
    public sealed partial class MainPage : Page
    {
        public string EventString { get; set; }
        private MainViewModel _mainViewModel = null;

        private NavigationHelper _navigationHelper;

        private DataTransferManager _dataTransferManager;

        private ObservableCollection<Common.Event> eEvents = new ObservableCollection<Common.Event>();
        public ObservableCollection<Common.Event> Events
        {
            get { return eEvents; }
            set { eEvents = value; }
        }
        public class position
        {
            public Geopoint MapCenter
            {
                get { return mapCenter; }
                set { mapCenter = value; }
            }

            private Geopoint mapCenter;
        }
        public position geo = new position();
        private BasicGeoposition currentGeo = new BasicGeoposition();

        private Geolocator geolocator = new Geolocator();
            


        public MainPage()
        {
            CurrentUser.PictureUrl = "ms-appx:///Assets/user.jpg";
            Section1ViewModel.UserName = CurrentUser.UserName;
            Section1ViewModel.PictureUrl = CurrentUser.PictureUrl;
            this.InitializeComponent();
            update();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            _navigationHelper = new NavigationHelper(this);

            _mainViewModel = _mainViewModel ?? new MainViewModel();

            ApplicationView.GetForCurrentView().
                SetDesiredBoundsMode(ApplicationViewBoundsMode.UseVisible);
        }

        /*private DependencyObject FindChildControl<T>(DependencyObject control, string ctrlName)
        {
            int childNumber = VisualTreeHelper.GetChildrenCount(control);
            for (int i = 0; i < childNumber; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(control, i);
                FrameworkElement fe = child as FrameworkElement;
                // Not a framework element or is null
                if (fe == null) return null;

                if (child is T && fe.Name == ctrlName)
                {
                    // Found the control so return
                    return child;
                }
                else
                {
                    // Not found it - search children
                    DependencyObject nextLevel = FindChildControl<T>(child, ctrlName);
                    if (nextLevel != null)
                        return nextLevel;
                }
            }
            return null;
        }
        */
        
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
                    /*Geoposition geoposition = await geolocator.GetGeopositionAsync(
                        maximumAge: TimeSpan.FromMinutes(1),
                        timeout: TimeSpan.FromSeconds(10)
                    );
                    currentGeo.Latitude = geoposition.Coordinate.Latitude;
                    currentGeo.Longitude = geoposition.Coordinate.Longitude;

                    geo.MapCenter = new Geopoint(currentGeo);
                    (this.MapSection.FindName("myMapControl") as MapControl).Center = MapCenter;*/
                    
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
        public MainViewModel MainViewModel
        {
            get { return _mainViewModel; }
        }

        public NavigationHelper NavigationHelper
        {
            get { return _navigationHelper; }
        }
        private void OnSectionsInViewChanged(object sender, SectionsInViewChangedEventArgs e)
        {
            var selectedSection = Container.SectionsInView.FirstOrDefault();
            if (selectedSection != null)
            {
                MainViewModel.SelectedItem = selectedSection.DataContext as ViewModelBase;
            }
        }

        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.
        /// Этот параметр обычно используется для настройки страницы.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;
            _navigationHelper.OnNavigatedTo(e);
            await MainViewModel.LoadDataAsync();
            
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);
            _dataTransferManager.DataRequested -= OnDataRequested;
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var viewModel = MainViewModel.SelectedItem;
            if (viewModel != null)
            {
                viewModel.GetShareContent(args.Request);
            }
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
