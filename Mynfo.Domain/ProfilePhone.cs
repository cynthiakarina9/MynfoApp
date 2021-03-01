namespace Mynfo.Domain
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProfilePhone
    {
        [Key]
        public int ProfilePhoneId { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(20, ErrorMessage = "The field {0} only can contains a maxium of {1} characters lenght.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(20, ErrorMessage = "The field {0} only can contains a maxium of {1} characters lenght.")]
        public string Number { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual ICollection<Box_ProfilePhone> Box_ProfilePhone { get; set; }

        [JsonIgnore]
        public virtual bool Exist { get; set; }
    }
}
