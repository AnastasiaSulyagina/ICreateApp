using AppStudio.ViewModels;
using AppStudio.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;


namespace Common
{
    public static class CurrentUser
    {
        public static int id { get; private set; }
        public static string PictureUrl { get; set; }
        public static string token { get; private set; }
        public static bool isAuthorized { get; private set; }
        public static string UserName { get; set; }
        public static ObservableCollection<Common.User> Friends { get { GetFriends(); return myf; } }
        public static ObservableCollection<Common.User> myf;
        public static string friends;
        public static void Unauthorize()
        {
            isAuthorized = false;
            UserName = "anonymous";//;
            token = "";
        }
        public static void Authorize(string newToken, string name)
        {
            token = newToken;
            isAuthorized = true;
            UserName = name;
            GetFriends();
            //Section1ViewModel.UserName = UserName;
            //Section1ViewModel.PictureUrl = PictureUrl;
        }

        public static async void GetFriends()
        {
            var friend1 = JsonConvert.SerializeObject(new 
            {UserId = 66, UserName = "Michail", Photo = "ms-appx:///Assets/DataImages/unknown.jpg"}); 
            var friend2 = JsonConvert.SerializeObject(new 
            {UserId = 67, UserName  = "Philipp",Photo = "ms-appx:///Assets/DataImages/unknown.jpg"});
            friends = '[' + friend1 + ',' +  friend2 + ']';
            var FriendList = JsonConvert.DeserializeObject<ObservableCollection<User> >(friends);
            foreach (User friend in FriendList)
            {
                friend.Photo = "ms-appx:///Assets/DataImages/unknown.jpg";
            }
            myf = FriendList;
        }
    }
}
