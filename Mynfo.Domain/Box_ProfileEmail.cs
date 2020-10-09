namespace Mynfo.Domain
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    public class Box_ProfileEmail
    {
        [Key]
        public int Box_ProfileEmailId { get; set; }

        public int BoxId { get; set; }
        [JsonIgnore]
        public virtual Box Box { get; set; }

        public int ProfileEmailId { get; set; }

        [JsonIgnore]
        public virtual ProfileEmail ProfileEmail { get; set; }
    }
}
