using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeRealm_Project.Models
{
    public class User
    {

       
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            [Required]
            [MaxLength(50)]
            public string FirstName { get; set; }

            [Required]
            [MaxLength(50)]
            public string LastName { get; set; }

            [Required]
            [MaxLength(256)]
            [Index(IsUnique = true)]
            public string Email { get; set; }

            [Required]
            [MaxLength(128)]
            public string Password { get; set; }


        [Required]
        [Column("Picture")]
        
        public string Picture { get; set; } = "Default_picture.jpg";


        [Required]
            [MaxLength(20)]
            public string PhoneNumber { get; set; }

            public ICollection<Booking> Bookings { get; set; }

            public ICollection<Property> Properties { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
    
}