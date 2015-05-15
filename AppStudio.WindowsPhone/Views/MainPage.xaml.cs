using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections.ObjectModel;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// Документацию по шаблону элемента пустой страницы см. по адресу http://go.microsoft.com/fwlink/?LinkID=390556

namespace AppStudio.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public class MyClass
        {
            private string mName;
            private int mValue;
            public string Name
            {
                get { return mName; }
            }
            public int Value
            {
                get { return mValue; }
            }
            public MyClass(string name, int value)
            {
                mName = name;
                mValue = value;
            }
        }

        private ObservableCollection<MyClass> mMyList = new ObservableCollection<MyClass>();
        public ObservableCollection<MyClass> MyList
        {
            get { return mMyList; }
        }
        public MainPage()
        {
            this.InitializeComponent();
            update();

        }
        private ObservableCollection<Common.Event> eEvents = new ObservableCollection<Common.Event>();
        public ObservableCollection<Common.Event> Events
        {
            get { return eEvents; }
        }

        private async void update()
        {
            
            var cts = new CancellationTokenSource();
            //this.updateProgressBar.IsIndeterminate = true;
            var lastString = "";
            do
            {
                var JsnString = await Common.ServerAPI.GetEvents();
                var deser = JsonConvert.DeserializeObject<ObservableCollection<Common.Event>>(JsnString);
                if (!lastString.Equals(JsnString))
                {
                    eEvents.Clear();
                    foreach(var elem in deser)
                    {
                        eEvents.Add(new Common.Event(elem.EventId, elem.LocationCaption.Substring(0, Math.Min(30, elem.LocationCaption.Length))+"...", new Common.User(elem.User.UserName, elem.User.UserId, elem.User.Photo), elem.EventDate, elem.DateCreate, elem.Latitude, elem.Longitude, elem.Description));
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

        
    }
}
