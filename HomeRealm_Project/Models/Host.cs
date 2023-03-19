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

        public virtual User User { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }

}