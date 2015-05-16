using AppStudio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class CurrentUser
    {
        private static string DefaultPicture = "/Assets/DataImages/User.jpg";
        public static int id { get; private set; }
        public static string PictureUrl { get; set; }
        public static string token { get; private set;}
        public static bool isAuthorized { get;  private set;}
        public static string UserName { get; private set;}
        private static string Friends;

        public static void Unauthorize()
        {
            isAuthorized = false;
            Section1ViewModel.UserName = "anonymous";
            token = "";
        }
        public static void Authorize(string newToken, string name)
        {
            token = newToken;
            isAuthorized = true;
            UserName = name;
            Section1ViewModel.UserName = UserName;
            Section1ViewModel.PictureUrl = PictureUrl; 
        }

        public static async Task<string> GetFriends()
        {
            return Friends = await ServerAPI.GetFriends();
        }
    }
}
