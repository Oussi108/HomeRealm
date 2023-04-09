using HomeRealm_Project.Models;
using System;
using System.Collections.Generic;
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
        public ActionResult UpgradeHost()
        {
            return View();
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