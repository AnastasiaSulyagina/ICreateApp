using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class User
    {
        public string UserName { get; set; }
        public int UserId { get; set; }

        public string Photo { get; set; }
        List<User> Friends { get; set; }
        //string un;
        //int ui;
        //string p;

        public User(string userName = "", int userId = 0, string photo = null)
        {
            UserName = userName;
            UserId = userId;
            if (photo == null) Photo = "/Assets/user.jpg";
            else Photo = photo;
        }
    }
}
