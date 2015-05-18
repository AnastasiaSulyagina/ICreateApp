using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Comment
    {
        

        public int CommentId { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public DateTime DateCreate { get; set; }

        public Comment(int commentId, int userId, string text, DateTime dateCreate)
        {
            User = new User();
            CommentId = commentId;
            User.UserId = userId;
            Text = text;
            DateCreate = dateCreate;
        }
    }
}
