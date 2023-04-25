using HomeRealm_Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeRealm_Project.Controllers
{
    public class SignUpController : Controller
    {
        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UpgradeToHost()
        {
            return View();
        }
        public ActionResult SubmitHostUpgrade(FormCollection FC)
        {

            HttpPostedFileBase file = Request.Files["IdPic"];

            if (file != null && file.ContentLength > 0)
            {
                int id = (int)Session["iduser"];
                // Do something with the uploaded file
                string fileName = id+ "_" +Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/images/idPics"), fileName);
                file.SaveAs(path);
                string bio = FC["Bio"];
                
                string country = FC["country"];
                string pic = fileName;

                string dateString = FC["birthday"];
                DateTime date = DateTime.Parse(dateString);

                using (var db = new MydbContext())
                {
                            var u = new Host {User = db.Users.FirstOrDefault(e => e.Id == 1),HostId = 1,Bio = bio,Country=country,IDPicture=pic,Birthday=date,Verified=false};
                            db.Hosts.Add(u);
                            db.SaveChanges();

                }
            }
            
            return RedirectToAction("Index", "Home");

        }
        public ActionResult SubmitSignUp(FormCollection FC)
        {
            string firstname = FC["FirstName"];
            string lastname = FC["LastName"];
            string email = FC["Email"];
            string pass = FC["Password"];
            string num = FC["Phone"];

            

            using (var db = new MydbContext())
            {
                var USER = db.Users.FirstOrDefault(user => user.Email == email);
                if (USER == null)
                {
                    if (firstname == "" || lastname == "" || email == "" || pass == "" || num == "")
                    {
                        ViewBag.Message = "Please write all the required information.";
                       
                    }
                    else {
                        var u = new User { FirstName = firstname, LastName = lastname, Email = email, Password = pass, PhoneNumber = num };
                        db.Users.Add(u);
                        db.SaveChanges();

                        ViewBag.Message = "Welcome new user, account has been created successfully.";
                    }
                   
                }
                else
                {
                    ViewBag.Message = "This email already exists.";
                }
            }

            return View("Index");
        }

    }
}