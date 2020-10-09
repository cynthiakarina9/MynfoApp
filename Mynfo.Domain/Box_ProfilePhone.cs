namespace Mynfo.Domain
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    public class Box_ProfilePhone
    {
        [Key]
        public int Box_ProfilePhoneId { get; set; }

        public int BoxId { get; set; }
        [JsonIgnore]
        public virtual Box Box { get; set; }

        public int ProfilePhoneId { get; set; }

        [JsonIgnore]
        public virtual ProfilePhone ProfilePhone { get; set; }
    }
}
