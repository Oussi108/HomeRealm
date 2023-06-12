using HomeRealm_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeRealm_Project.Controllers
{
    public class SettingController : Controller
    {
        // GET: Setting
        public ActionResult Index()
        {
            SettingModel CurrentUser;
            int id = int.Parse(Session["iduser"].ToString());
            using (MydbContext db = new MydbContext()) {
                CurrentUser = new SettingModel() { user = db.Users.Find(Session["iduser"]), host = db.Hosts.Where(d => d.User.Id == id).FirstOrDefault() };
                if (CurrentUser.user is null) {
                    return Redirect("Home");
                }
            
            }
            return View(CurrentUser);
        }
        [HttpPost]
        
        public ActionResult Submitchange(FormCollection formCollection)
        {
            int id = int.Parse(Session["iduser"].ToString());
            MydbContext db = new MydbContext();
            // Retrieve the current user from the database
            var currentUser = db.Users.FirstOrDefault(u => u.Id == id);

            // Update the user attributes based on the form data
            currentUser.FirstName = formCollection["Firstname"];
            currentUser.LastName = formCollection["Lastname"];
            currentUser.Email = formCollection["Email"];
           

            // Check if the password fields are not empty
            if (!string.IsNullOrEmpty(formCollection["oldpass"]) && !string.IsNullOrEmpty(formCollection["newpass"]) && !string.IsNullOrEmpty(formCollection["newpassC"]))
            {
                string oldPassword = formCollection["oldpass"];
                string newPassword = formCollection["newpass"];
                string confirmPassword = formCollection["newpassC"];

               if(oldPassword== currentUser.Password && newPassword== confirmPassword) { 
                // Update the user's password
                currentUser.Password = newPassword;
            }
            }

            // Save the changes to the database
            db.SaveChanges();

            // Redirect to a success page or return a success message
            return RedirectToAction("Index");
        }






    }
}