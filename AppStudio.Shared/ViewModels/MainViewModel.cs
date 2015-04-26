using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.NetworkInformation;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class MainViewModel : BindableBase
    {
       private Section1ViewModel _section1Model;
       private Section2ViewModel _section2Model;
       private HtmlViewModel _htmlModel;
        private PrivacyViewModel _privacyModel;

        private ViewModelBase _selectedItem = null;

        public MainViewModel()
        {
            _selectedItem = Section1Model;
            _privacyModel = new PrivacyViewModel();

        }
 
        public Section1ViewModel Section1Model
        {
            get { return _section1Model ?? (_section1Model = new Section1ViewModel()); }
        }
 
        public Section2ViewModel Section2Model
        {
            get { return _section2Model ?? (_section2Model = new Section2ViewModel()); }
        }
 
        public HtmlViewModel HtmlModel
        {
            get { return _htmlModel ?? (_htmlModel = new HtmlViewModel()); }
        }

        public void SetViewType(ViewTypes viewType)
        {
            Section1Model.ViewType = viewType;
            Section2Model.ViewType = viewType;
            HtmlModel.ViewType = viewType;
        }

        public ViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                UpdateAppBar();
            }
        }

        public Visibility AppBarVisibility
        {
            get
            {
                return SelectedItem == null ? AboutVisibility : SelectedItem.AppBarVisibility;
            }
        }

        public Visibility AboutVisibility
        {

         get { return Visibility.Visible; }
        }

        public void UpdateAppBar()
        {
            OnPropertyChanged("AppBarVisibility");
            OnPropertyChanged("AboutVisibility");
        }

        /// <summary>
        /// Load ViewModel items asynchronous
        /// </summary>
        public async Task LoadDataAsync(bool forceRefresh = false)
        {
            var loadTasks = new Task[]
            { 
                Section1Model.LoadItemsAsync(forceRefresh),
                Section2Model.LoadItemsAsync(forceRefresh),
                HtmlModel.LoadItemsAsync(forceRefresh),
            };
            await Task.WhenAll(loadTasks);
        }

        //
        //  ViewModel command implementation
        //
        public ICommand RefreshCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    await LoadDataAsync(true);
                });
            }
        }

        public ICommand AboutCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("AboutThisAppPage");
                });
            }
        }
        public ICommand LoginCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("LoginPage");
                });
            }
        }

        public ICommand AddEventCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("AddEventPage");
                });
            }
        }

        public ICommand PrivacyCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateTo(_privacyModel.Url);
                });
            }
        }
    }
}
