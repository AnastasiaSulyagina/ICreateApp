using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    class User
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
        List<User> Friends { get; set; }
    }
}
