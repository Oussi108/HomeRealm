using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeRealm_Project.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SubmitLogin(FormCollection  FC)
        {
            string pass = FC["Password"];
            string email = FC["Email"];
            
            if (pass == "1")
                Session["log"] = "Hello, world!";

            else
                Session["log"] = "Hello, world!";

            return RedirectToAction("Index", "Home");
        }
    }
}