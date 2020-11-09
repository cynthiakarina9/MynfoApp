namespace Mynfo.Models
{
    using SQLite;
    public class ForeingProfile
    {
        [PrimaryKey]
        [AutoIncrement]
        public int ForeingProfileId { get; set; }

        public int BoxId { get; set; }

        public int UserId { get; set; }

        public string ProfileName { get; set; }

        public string value { get; set; }

        public string ProfileType { get; set; }
    }
}
