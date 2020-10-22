using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Mynfo.Domain
{
    public class Box_ProfileSM
    {
        [Key]
        public int Box_ProfileSMId { get; set; }

        public int BoxId { get; set; }
        [JsonIgnore]
        public virtual Box Box { get; set; }

        public int ProfileMSId { get; set; }

        [JsonIgnore]
        public virtual ProfileSM ProfileSM{ get; set; }
    }
}
