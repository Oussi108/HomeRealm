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
            User CurrentUser;
            using (MydbContext db = new MydbContext()) {
                CurrentUser = db.Users.Find(Session["iduser"]);
                if (CurrentUser is null) {
                    return Redirect("Home");
                }
            
            }
            return View(CurrentUser);
        }
        public ActionResult ChangeUser()
        {

            return View();
        }

    }
}