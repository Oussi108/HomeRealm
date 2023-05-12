using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeRealm_Project.Models
{
    [Table("PropertyImages")]
    public class PropertyImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("PropertyId")]
        [ForeignKey("Property")]
        public int PropertyId { get; set; }

        [Required]
        [Column("DateUploaded")]
        public DateTime DateUploaded { get; set; }

        [Required]
        [StringLength(255)]
        [Column("ImageUrl")]
        public string ImageUrl { get; set; }

        public virtual Property Property { get; set; }
    }
}