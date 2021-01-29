namespace Mynfo.Models
{
    using SQLite;
    using System;

    public class ForeingBox
    {
        [PrimaryKey]
        [AutoIncrement]
        public int BoxIdForeing { get; set; }

        public int BoxId { get; set; }

        public int UserId { get; set; }

        public DateTime Time { get; set; }

        public string ImagePath { get; set; }

        public int UserTypeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath))
                {
                    return "no_image";
                }

                if (this.UserTypeId == 1)
                {
                    return string.Format(
                        "https://mynfoapi1.azurewebsites.net/{0}",
                        ImagePath.Substring(1));
                }

                return ImagePath;
            }
        }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        public override string ToString()
        {
            return FirstName;
        }
    }
}