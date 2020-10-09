namespace Mynfo.Domain
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Box
    {
        [Key]
        public int BoxId { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(20, ErrorMessage = "The field {0} only can contains a maxium of {1} characters lenght.")]
        public string Name { get; set; }

        public bool BoxDefault { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual ICollection<Box_ProfileSM> Box_ProfileSM { get; set; }

        [JsonIgnore]
        public virtual ICollection<Box_ProfileEmail> Box_ProfileEmail { get; set; }
    }
}