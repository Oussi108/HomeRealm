using HomeRealm_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeRealm_Project.Controllers
{
    public class HomeController : Controller
    {
        

        public ActionResult Index()
        {
            PropertiesViewModel viewModel;
            using (var db = new MydbContext()) { 
                var properties = db.Properties.ToList().Take(3).ToList(); // Retrieve properties from your data source

              viewModel = new PropertiesViewModel
            {
                Properties = properties
            };
            }
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult DeleteSession()
        {
            Session.Remove("iduser");
            Session.Remove("rule");
            Session.Remove("usermail");
            

            return RedirectToAction("Index");
        }
        
    }
}