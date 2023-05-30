using HomeRealm_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeRealm_Project.Controllers
{
    public class ListingController : Controller
    {
        // GET: Listing
        public ActionResult Index()
        {
            PropertiesViewModel viewModel;
            using (var db = new MydbContext())
            {
                var properties = db.Properties.ToList(); // Retrieve properties from your data source

                viewModel = new PropertiesViewModel
                {
                    Properties = properties
                };
            }
            return View(viewModel);
        }
        public ActionResult Details(int id = 1)
        {

            PropertyWithImagesModel viewModel;
            using (var db = new MydbContext())
            {
                var Property = db.Properties.Find(id); // Retrieve properties from your data source
                viewModel = new PropertyWithImagesModel { Ourproperty = Property };


            }

            return View(viewModel);
        }
        public ActionResult book(int id = 1)
        {
            if (Session["iduser"] is null)
            {
                return RedirectToAction("index", "home");
            }
            ViewBag.idProperty = id;

            return View();
        }
        [HttpPost]
        public ActionResult Submitbooking(FormCollection fc)
        {
            int idpro = int.Parse(fc["IdProperty"]);
            int iduser = int.Parse(Session["iduser"].ToString());
            DateTime checkin = DateTime.Parse(fc["checkin"]);
            DateTime checkout = DateTime.Parse(fc["checkout"]);
            AddBooking(idpro, iduser, checkin, checkout);
            return RedirectToAction("index", "home");

        }
        public void AddBooking(int propertyId, int userId, DateTime checkInDate, DateTime checkOutDate)
        {
            // Create a new instance of the Booking class
            var newBooking = new Booking
            {
                PropertyId = propertyId,
                UserId = userId,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate
                // Set other properties as needed
            };

            // Add the new booking to the appropriate data context or repository
            // and save changes to the database
            using (var context = new MydbContext()) // Replace "YourDbContext" with your actual DbContext class
            {
                context.Bookings.Add(newBooking);
                context.SaveChanges();
            }
        }
        public ActionResult Search(FormCollection FC)
        {

            string location = FC["location"];
            int capacity = Convert.ToInt32(FC["capacity"]);
            DateTime? checkin = null;
            DateTime? checkout = null;

            if (DateTime.TryParse(FC["checkin"], out DateTime checkinValue))
            {
                checkin = checkinValue;
            }

            if (DateTime.TryParse(FC["checkout"], out DateTime checkoutValue))
            {
                checkout = checkoutValue;
            }

            var properties = SearchProperties(location, checkin, checkout, capacity);

            PropertiesViewModel viewModel;

            viewModel = new PropertiesViewModel
            {
                Properties = properties
            };


            return View("index", viewModel);
        }
        public List<Property> SearchProperties(string location, DateTime? checkIn, DateTime? checkOut, int? capacity)
        {
            using (var db = new MydbContext())
            {
                var query = db.Properties.Where(p => p.IsAvailable);

                if (!string.IsNullOrEmpty(location))
                {
                    query = query.Where(p => p.City == location);
                }

                if (checkIn != null && checkOut != null)
                {
                    query = query.Where(p => p.Bookings.All(b =>
                        (checkIn >= b.CheckOutDate || checkOut <= b.CheckInDate)));
                }

                if (capacity != null)
                {
                    query = query.Where(p => p.Capacity >= capacity);
                }

                var properties = query.ToList();

                return properties;
            }
        }
    }
    
}