namespace Mynfo.Models
{
    using SQLite;
    public class Profile
    {
        [PrimaryKey]
        [AutoIncrement]
        public int ProfileLocalId { get; set; }
        public int UserId { get; set; }
        public string ProfileName { get; set; }
        public string value { get; set; }
        public string ProfileType { get; set; }
        public int RedSocialId { get; set; }
        public string Logo { get; set; }
        public int ProfileId { get; set; }
        public bool Exist { get; set; }
    }
}
