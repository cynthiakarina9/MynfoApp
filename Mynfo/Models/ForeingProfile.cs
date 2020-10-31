namespace Mynfo.Models
{
    using SQLite;
    public class ForeingProfile
    {
        [PrimaryKey]
        public int ForeingProfileId { get; set; }

        public int IdBox { get; set; }

        public int UserId { get; set; }

        public string ProfileName { get; set; }

        public string value { get; set; }
    }
}
