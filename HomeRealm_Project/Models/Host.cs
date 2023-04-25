using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeRealm_Project.Models
{
    [Table("Host")]
    public class Host
    {
        [Key]
        [ForeignKey("User")]
        public int HostId { get; set; }
        [Required]
        public virtual User User { get; set; }
        [Required]

        public string IDPicture { get; set; }
        [Required]
        public string Bio { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public bool Verified { get; set; } = false;
        public virtual ICollection<Property> Properties { get; set; }
    }

}