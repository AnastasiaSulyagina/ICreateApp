using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    class Event
    {
        private User Organizer { get; set; }
        private int Id { get; set; }
        //private GeoCoordinate coordinate { get; set; }
        private string Description { get; set; }
        private string ShortDescription { get; set; }
        //private string MySquareDescriprion { get; set; }
        private int Category { get; set; }
        private DateTime DateTime { get; set; }
        private double Latitude { get; set; }
        private double Longitude { get; set; }
        private List<Comment> comments { get; set; }

        public Event(DateTime time, double latitude, double longitude, String description, int category)
        {
            Id = 0;
            //MySquareDescriprion = "";
            //for (int i = 0; i < description.Length; ++i)
            //{
            //    MySquareDescriprion += description[i];
            //    if (((i % 20) == 0) && (i > 0))
            //    {
            //        MySquareDescriprion += '\n';
            //    }
            //    if (i == 100)
            //    {
            //        break;
            //    }
            //}

            DateTime = time;
            Latitude = latitude;
            Longitude = longitude;
            Description = description;
            Category = category;
            ShortDescription = Description;
            //this.Coordinate = new GeoCoordinate(latitude, longitude);
        }
    }
}
