using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeRealm_Project.Models
{
    public class BookingHostModel
    {
        public List<Booking> MyRequests { get; set; }
        public BookingHostModel(int idHost)
        {
            MydbContext db = new MydbContext();
            MyRequests = db.Bookings.Where(b => b.Property.HostId == idHost).ToList();
        }
    }
}