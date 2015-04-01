using System;

using Windows.ApplicationModel;

namespace AppStudio.ViewModels
{
    public class AboutThisAppViewModel
    {
        public string Publisher
        {
            get
            {
                return "AppStudio";
            }
        }

        public string AppVersion
        {
            get
            {
                return string.Format("{0}.{1}.{2}.{3}", Package.Current.Id.Version.Major, Package.Current.Id.Version.Minor, Package.Current.Id.Version.Build, Package.Current.Id.Version.Revision);
            }
        }

        public string AboutText
        {
            get
            {
                return "Do you own a restaurant, or have a favorite place you go every week? Use this tem" +
    "plate to show your customers or your friends about the delicious meal they\'ll fi" +
    "nd there. With easy-to-navigate sections and pictures for starters, main courses" +
    " and desserts";
            }
        }
    }
}

