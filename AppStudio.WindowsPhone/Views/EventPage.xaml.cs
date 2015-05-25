using System;
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

using Newtonsoft.Json;
using Newtonsoft.Json;

using AppStudio;
using Common;

namespace AppStudio.Views
{
    public sealed partial class EventPage : Page
    {
        private ObservableCollection<Comment> eComments = new ObservableCollection<Comment>();
        public ObservableCollection<Comment> Comments
        {
            get { return eComments; }
            set { eComments = value; }
        }

        public Common.Event Event;
        public EventPage()
        {
            this.InitializeComponent();
            
        }
        private async void update()
        {
            var cts = new CancellationTokenSource();
            var lastString = "";
            do
            {
                string JsnString;
                try
                {
                    JsnString = await ServerAPI.GetComments(Event.EventId);
                }
                catch (Exception e)
                {
                    string s = e.ToString();
                    continue;
                }
                
                
                var deser = JsonConvert.DeserializeObject<ObservableCollection<Comment>>(JsnString);

                if (!lastString.Equals(JsnString))
                {
                    eComments.Clear();
                    foreach (var elem in deser)
                    {
                        eComments.Add(new Comment(elem.CommentId, elem.User.UserId, elem.Text, elem.DateCreate));
                    }
                }
                await loop(cts.Token);
                cts.Cancel();
                lastString = JsnString;
            } while (true);
        }

        private async Task<int> loop(CancellationToken ct)
        {
            await Task.Delay(1000);
            return 1;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Object obj = e.Parameter;
            Event = (Event)obj;
            UsernameBlock.Text = Event.User.UserName;
            DateCreateBlock.Text = Event.DateCreate.ToString();
            DescriptionBlock.Text = Event.ShortDescription;
            LocationCaptionBlock.Text = Event.ShortLocationCaption;
            EventDateBlock.Text = Event.EventDate.ToString();

            update();
        }

        private void newCommentButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
