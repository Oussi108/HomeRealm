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
        public ActionResult Details()
        {   
            return View();
        }
    }
}