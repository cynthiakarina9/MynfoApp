namespace Mynfo.Domain
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UserTags
    {
        [Key]
        public int UserTagsId { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        public int TagId { get; set; }

        [Display(Name = "Name Tag")]
        [Required(ErrorMessage = "The field {0} is requiered.")]
        [MaxLength(15, ErrorMessage = "The field {0} only can contains a maxium of {1} characters lenght.")]
        public string Name { get; set; }
    }
}
