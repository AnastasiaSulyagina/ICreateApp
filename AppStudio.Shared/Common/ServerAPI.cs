using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using System.IO;

namespace Common
{
    public class ServerAPI
    {
        private static BasicGeoposition coordinate = new BasicGeoposition();//59.875585, 29.825813);
        
        private static string LoginUrl = "Token";
        private static string RegisterUrl = "api/Account/Register";
        private static string GetEventsUrl = "api/Events";
        private static string AddEventUrl = "api/Events";
        private static string AddCommentUrl = "api/eventComments";
        private static string SubscribeUrl = "api/Friends/Follow";
        private static string GetFriendsUrl = "api/Friends/my/m";
        private static string SiteUrl = "http://icreate.azurewebsites.net/";


        public static async Task<bool> Login(string login, string password)
        {
            var client = new HttpClient();
            var data = "grant_type=password&username=" + login + "&password=" + password;
            var content = new StringContent(data.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
            try
            {
                var result = await client.PostAsync(SiteUrl + LoginUrl, content);
                var message = result.ReasonPhrase;
                var responseString = await result.Content.ReadAsStringAsync();
                var user = JObject.Parse(responseString);
                
                CurrentUser.Authorize(user["access_token"].Value<string>(), login);
                
            }
            catch (WebException we)
            {
                CurrentUser.Unauthorize();
                return false;
            }
            return true;
        }

        public static async Task<bool> Register(string login, string password, string confirm)
        {
            var client = new HttpClient();
            var data = JsonConvert.SerializeObject(new
            {
                UserName = login,
                Password = password,
                ConfirmPassword = confirm
            });
            var content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
            try
            {
                var result = await client.PostAsync(SiteUrl + RegisterUrl, content);
                var responseString = await result.Content.ReadAsStringAsync();
                await Login(login, password);
            }
            catch (WebException we)
            {
                CurrentUser.Unauthorize();
            }
            return true;
        }

        public static async void AddEvent(string description, DateTime datetime)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentUser.token);
            try
            {
                var data = JsonConvert.SerializeObject(new
                {
                    Latitude = coordinate.Latitude.ToString().Replace(",", "."),
                    Longitude = coordinate.Longitude.ToString().Replace(",", "."),
                    Description = description,
                    EventDate = datetime.ToString()
                });

                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var ss = data.ToString();
                var result = await client.PostAsync(SiteUrl + AddEventUrl, content);
                var message = result.ReasonPhrase;
                var s = await content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        // waiting for server
        //public static async void UploadPicture(string picUrl) 
        //{
        //    var someClient = new HttpClient();
        //    someClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentUser.token);
        //    string sdata;
        //    var fileIds = new string[0];
        //    var link = "";
        //    var ID = "";
        //    var url = await someClient.GetStringAsync(new Uri(SiteUrl + "api/Endpoints/GetUploadUrl/UpdateUserPic"));
        //    url = url.Trim(new char[] { '\"' }).TrimStart(new char[] { '/' });
        //    using (var client = new HttpClient())
        //    {
        //        MultipartFormDataContent form = new MultipartFormDataContent();
        //        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + CurrentUser.token);
        //        var fs = new FileStream(picUrl, FileMode.Open);
        //        form.Add(new StreamContent(fs), "file", "file.jpg");
        //        var response = await client.PostAsync(SiteUrl + url, form);
        //        sdata = await response.Content.ReadAsStringAsync();
        //    }
        //    using (var client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + CurrentUser.token);
        //        var content = new StringContent(sdata, Encoding.UTF8, "application/json");
        //        var response = await client.PostAsync(SiteUrl + "api/Endpoints/SaveUploadedFile/UpdateUserPic", content);
        //        sdata = await response.Content.ReadAsStringAsync();
        //        var smth = JObject.Parse(sdata);
        //        ID = smth["Id"].Value<string>();
        //        link = smth["Url"].Value<string>();
        //    }
        //    try
        //    {
        //        var data = JsonConvert.SerializeObject(new
        //        {
        //            UserFileId = ID
        //        });
        //        var content = new StringContent(data, Encoding.UTF8, "application/json");
        //        var result = await someClient.PostAsync(SiteUrl + "api/Account/UpdateUserPic", content);
        //    }
        //    catch (Exception e)
        //    { }
        //}

        public static async Task<string> GetEvents()
        {
            var client = new HttpClient();
            var result = await client.GetStringAsync(SiteUrl + GetEventsUrl);
            return result;
        }

        public static async Task<string> GetFriends()
        {
            var client = new HttpClient();
            var result = await client.GetStringAsync(SiteUrl + GetFriendsUrl);
            return result;
        }

        public static async void AddComment(string text) 
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentUser.token);
            try
            {
                var data = JsonConvert.SerializeObject(new
                {
                    UserId = CurrentUser.id,
                    Text = text,
                    DateTime = DateTime.Now
                });
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(SiteUrl + AddCommentUrl, content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public static async void Follow(string name)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentUser.token);

            try
            {
                var data = JsonConvert.SerializeObject(new
                {
                    SubscribedTo = name,
                });

                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(SiteUrl + SubscribeUrl + '/', content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        
    }
}
