using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Event
    {
        public User User { get { return mUser; } }
        public int EventId { get { return mEventId; } }
        //private GeoCoordinate coordinate { get; set; }
        public string Description { get { return mDescription; } }
        //private string ShortDescription { get; set; }
        //private string MySquareDescriprion { get; set; }
        //private int Category { get; set; }
        public DateTime EventDate { get { return mEventDate; } }
        public DateTime DateCreate { get { return mDateCreate; } }
        public string LocationCaption { get { return mLocationCaption; } }
        public double Latitude { get { return mLatitude; } }
        public double Longitude { get { return mLongitude; } }
        //private List<Comment> comments { get; set; }

        private User mUser;
        private int mEventId;
        //private GeoCoordinate coordinate { get; set; }
        private string mDescription;
        //private string ShortDescription { get; set; }
        //private string MySquareDescriprion { get; set; }
        //private int Category { get; set; }
        private DateTime mEventDate;
        private DateTime mDateCreate;
        private string mLocationCaption;
        private double mLatitude;
        private double mLongitude;

        public Event(int eventId, string locationCaption, User user, DateTime eventDate, DateTime dateCreate, double latitude, double longitude, String description)
        {
            mEventId = eventId;
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

            mUser = user;
            mEventDate = eventDate;
            mDateCreate = dateCreate;
            mLocationCaption = locationCaption;
            mLatitude = latitude;
            mLongitude = longitude;
            mDescription = description;
            //ShortDescription = description;
            //this.Coordinate = new GeoCoordinate(latitude, longitude);
        }
    }
}
