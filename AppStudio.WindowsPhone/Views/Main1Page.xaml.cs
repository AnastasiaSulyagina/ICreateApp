using System;
using System.Net.NetworkInformation;

using Windows.ApplicationModel.DataTransfer;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using AppStudio.Services;
using AppStudio.ViewModels;
using System.Collections.Generic;
using Common;
using Newtonsoft.Json;

namespace AppStudio.Views
{
    public sealed partial class Main1Page : Page
    {
        private NavigationHelper _navigationHelper;
        private List<User> Friends;
        private DataTransferManager _dataTransferManager;

        public Main1Page()
        {
            this.InitializeComponent();
            _navigationHelper = new NavigationHelper(this);

            Main1Model = new Main1ViewModel();
            DataContext = this;

            ApplicationView.GetForCurrentView().
                SetDesiredBoundsMode(ApplicationViewBoundsMode.UseVisible);
        }

        private async void update()
        {

            string result = await CurrentUser.GetFriends();
            Friends = JsonConvert.DeserializeObject<List<User>>(result);
            //eventList.ItemsSource = Friends;
        }

        public Main1ViewModel Main1Model { get; private set; }

        public NavigationHelper NavigationHelper
        {
            get { return _navigationHelper; }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;

            _navigationHelper.OnNavigatedTo(e);
            await Main1Model.LoadItemsAsync();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);
            _dataTransferManager.DataRequested -= OnDataRequested;
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            if (Main1Model != null)
            {
                Main1Model.GetShareContent(args.Request);
            }
        }
    }
}
