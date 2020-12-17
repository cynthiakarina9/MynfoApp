namespace Mynfo.Domain
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ProfileEmail
    {
        [Key]
        public int ProfileEmailId { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(20, ErrorMessage = "The field {0} only can contains a maxium of {1} characters lenght.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(100, ErrorMessage = "The field {0} only can contains a maxium of {1} characters lenght.")]
        [Index("User_Email_Index", IsUnique = true)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual ICollection<Box_ProfileEmail> Box_ProfileEmail { get; set; }

        [JsonIgnore]
        public virtual bool Exist { get; set; }
    }
}