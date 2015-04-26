using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    static class CurrentUser
    {
        private static string token = "";
        private static bool isAuthorized = false;
        static User UserInfo;
        public static bool IsAuthorized()
        {
            return isAuthorized;
        }
        public static void Unauthorize()
        {
            isAuthorized = false;
            token = "";
        }
        public static void Authorize(string newToken, string name)
        {
            token = newToken;
            isAuthorized = true;
            UserInfo.UserName = name;
        }
        public static string GetToken()
        {
            return token;
        }
    }
}
