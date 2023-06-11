using HomeRealm_Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeRealm_Project.Controllers
{
    public class ListingMangmentController : Controller
    {
        // GET: ListingMangment
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SubmitAddHouse(FormCollection FC)
        {
            string path ="";
            HttpPostedFileBase file = Request.Files["Mainphoto"];
            if (file != null && file.ContentLength > 0)
            {
                int id = (int)Session["iduser"];
                // Do something with the uploaded file
                string fileName = id + "_" + Path.GetFileName(file.FileName)+ "_Main";
                 path = Path.Combine(Server.MapPath("~/images/Properties"), fileName);
                file.SaveAs(path);
                
              
            }
            
            var newProperty = new Property
            {
                Title = FC["Title"],
                Description = FC["Description"],
                Address = FC["Adress"],
                City = FC["City"],
                Country = FC["Country"],
                ImageUrl = path,
                Price = double.Parse(FC["Price"]),
                Capacity = int.Parse(FC["Capacity"]) ,
                IsAvailable = true,
                CreatedAt = DateTime.Now,
                HostId = (int)Session["userid"]
            };

            AddProperty(newProperty);
            return RedirectToAction("Index");
        }
        public ActionResult AddHouse()
        {
            

            return View();

        }
        public ActionResult Addphotos()
        {


            return View();

        }
        [HttpPost]
        public ActionResult SubmitPhotos()
        {
            // Get the uploaded files
            var files = Request.Files;

            // Process the uploaded files
            foreach (string fileName in files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                if (file != null && file.ContentLength > 0)
                {
                    // Get the file name and extension
                    string fileNameWithExtension = Path.GetFileName(file.FileName);
                    string extension = Path.GetExtension(fileNameWithExtension);

                    // Generate a unique file name to avoid overwriting existing files
                    string newFileName = Guid.NewGuid().ToString() + extension;

                    // Set the directory where you want to save the file
                    string directory = @"~/images/Properties";

                    // Create the directory if it doesn't exist
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    
                    // Save the file to disk
                    file.SaveAs(Path.Combine(directory, newFileName));
                    AddPropertyImage((int)Session["Property"], newFileName);
                    Session["Property"] = null;
                }
            }

            // Redirect to the success page
            return RedirectToAction("Index");
        }
        public void AddProperty(Property property)
        {
            using (var context = new MydbContext())
            {
                context.Properties.Add(property);
                context.SaveChanges();
            }
            Session["Property"] = property.Id;
        }
        public void AddPropertyImage(int propertyId, string imageUrl)
        {
            // Create a new PropertyImage object
            PropertyImage propertyImage = new PropertyImage
            {
                PropertyId = propertyId,
                DateUploaded = DateTime.Now,
                ImageUrl = imageUrl
            };

            // Add the new PropertyImage object to the database
            using (var dbContext = new MydbContext())
            {
                dbContext.PropertyImages.Add(propertyImage);
                dbContext.SaveChanges();
            }
        }
        public ActionResult ViewBooks()
        {
            BookingHostModel bk = null;
            if (!(Session["iduser"] is null))
            {
                 bk = new BookingHostModel(int.Parse(Session["iduser"].ToString()));
            }
            return View(bk);

        }
        public ActionResult ViewProperties()
        {
            PropertiesViewModel bk = null;
            if (!(Session["iduser"] is null))
            {
                int id = int.Parse(Session["iduser"].ToString());
                MydbContext db = new MydbContext();
                bk = new PropertiesViewModel() { Properties = db.Properties.Where(a => a.Host.User.Id == id).ToList() };
            }
            return View(bk);

        }
        [HttpPost]
        public ActionResult DeleteProperty(int id)
        {
            using (var db = new MydbContext())
            {
                // Find the property by ID
                var property = db.Properties.Find(id);

                if (property == null)
                {
                    // Property not found, handle the error or display a message
                    return RedirectToAction("index");
                }

                // Remove the property from the context and mark it for deletion
                db.Properties.Remove(property);
                db.SaveChanges();
            }

            // Redirect the user to the property list page or any other appropriate page
            return RedirectToAction("ViewProperties");
        }

        [HttpPost]
        
        public ActionResult EditPropertysub(FormCollection form)
        {
            MydbContext _dbContext = new MydbContext();
            // Retrieve the property ID from the form collection
            int propertyId = int.Parse(form["Id"]);

            // Retrieve the property by its ID
            var property = _dbContext.Properties.Find(propertyId);
            if (ModelState.IsValid)
            {
               

                if (property == null)
                {
                    // Property not found
                    return RedirectToAction("ViewProperties");
                }

                // Update the property with the new values from the form collection
                property.Title = form["Title"];
                property.Description = form["Description"];
                property.Address = form["Address"];
                property.City = form["City"];
                property.Country = form["Country"];
                property.Price = double.Parse(form["Price"]);
                property.Capacity = int.Parse(form["Capacity"]);
                property.IsAvailable = form["IsAvailable"] == "on";
                property.UpdatedAt = DateTime.Now;

                // Save the changes to the database
                _dbContext.SaveChanges();

                return RedirectToAction("ViewProperties");
            }

            return View("EditProperty", property);
        }


        public ActionResult EditProperty(int id)
        {
            MydbContext db = new MydbContext();
            // Retrieve the property from the database
            var property = db.Properties.Find(id);
            if (property == null)
            {
                // Property not found, handle accordingly (e.g., redirect to an error page)
                return RedirectToAction("ViewProperties", "ListingMangment");
            }

            // Pass the property to the view
            var model = property;

            return View(model);
        }
    }
}