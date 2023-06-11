using HomeRealm_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeRealm_Project.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            MydbContext db = new MydbContext();
            var hosts = db.Hosts.Where(host => host.Verified == false).ToList();
            HostListModel pd = new HostListModel() { Hosts = hosts };
            return View(pd);
        }
        public ActionResult ViewIDpicture(int id)
        {
            MydbContext db = new MydbContext();
            var host1 = db.Hosts.Find(id);
            
            return View(new HostModel() { Host = host1});
        }
    }
}