using System;
using System.Collections.Generic;
using System.Text;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.Devices.Geolocation;

namespace Common
{
    public class Event
    {
        public User User { get { return mUser; } }
        public int EventId { get { return mEventId; } }
        //private GeoCoordinate coordinate { get; set; }
        public string Description { get { return mDescription; } }
        public string ShortDescription { get { return mShortDescription; } }
        
        //private string ShortDescription { get; set; }
        //private string MySquareDescriprion { get; set; }
        //private int Category { get; set; }
        public DateTime EventDate { get { return mEventDate; } }
        public DateTime DateCreate { get { return mDateCreate; } }
        public string LocationCaption { get { return mLocationCaption; } }

        public string ShortLocationCaption { get { return mShortLocationCaption; } }
        public double Latitude { get { return mLatitude; } }
        public double Longitude { get { return mLongitude; } }
        public string AnchorPoint { get { return mAnchorPoint; } }
        public Visibility isVisible { get { return misVisible; } }
        public Geopoint Geopoint
        {
            get
            {
                return (new Geopoint(new BasicGeoposition()
                {
                    Latitude = mLatitude,
                    Longitude = mLongitude
                })
                );
            }
        }

        public Visibility misVisible = Visibility.Collapsed;
        //private List<Comment> comments { get; set; }
        private string mAnchorPoint = "0.5, 1";
        private User mUser;
        private int mEventId;
        //private GeoCoordinate coordinate { get; set; }
        private string mDescription;
        private string mShortDescription;
        private string mShortLocationCaption;
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
            mShortDescription = description.Substring(0, Math.Min(120, description.Length)) + ((120 < description.Length)?"...":"");
            mShortLocationCaption = locationCaption.Substring(0, Math.Min(30, locationCaption.Length)) + ((30 < locationCaption.Length) ? "..." : "");
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
