using System;
using System.Collections;
using Newtonsoft.Json;

namespace AppStudio.Data
{
    /// <summary>
    /// Implementation of the DessertsSchema class.
    /// </summary>
    public class DessertsSchema : BindableSchemaBase, IEquatable<DessertsSchema>, ISyncItem<DessertsSchema>
    {
        private string _title;
        private string _subtitle;
        private string _image;
        private string _description;
        [JsonProperty("_id")]
        public string Id { get; set; }

 
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
 
        public string Subtitle
        {
            get { return _subtitle; }
            set { SetProperty(ref _subtitle, value); }
        }
 
        public string Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }
 
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public override string DefaultTitle
        {
            get { return Title; }
        }

        public override string DefaultSummary
        {
            get { return Subtitle; }
        }

        public override string DefaultImageUrl
        {
            get { return Image; }
        }

        public override string DefaultContent
        {
            get { return Subtitle; }
        }

        override public string GetValue(string fieldName)
        {
            if (!String.IsNullOrEmpty(fieldName))
            {
                switch (fieldName.ToLowerInvariant())
                {
                    case "title":
                        return String.Format("{0}", Title); 
                    case "subtitle":
                        return String.Format("{0}", Subtitle); 
                    case "image":
                        return String.Format("{0}", Image); 
                    case "description":
                        return String.Format("{0}", Description); 
                    case "defaulttitle":
                        return DefaultTitle;
                    case "defaultsummary":
                        return DefaultSummary;
                    case "defaultimageurl":
                        return DefaultImageUrl;
                    default:
                        break;
                }
            }
            return String.Empty;
        }

        public bool Equals(DessertsSchema other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;
            return this.Id == other.Id;
        }

        public bool NeedSync(DessertsSchema other)
        {

            return this.Id == other.Id && (this.Title != other.Title || this.Subtitle != other.Subtitle || this.Image != other.Image || this.Description != other.Description);
        }

        public void Sync(DessertsSchema other)
        {
            this.Title = other.Title;
            this.Subtitle = other.Subtitle;
            this.Image = other.Image;
            this.Description = other.Description;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DessertsSchema);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
