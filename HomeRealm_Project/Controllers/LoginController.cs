﻿using HomeRealm_Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            string path = Server.MapPath("~/Admins.json");
            AdminService a = new AdminService(path);
            if (a.AdminExists(email,pass))
            {
                Session["Admin"] = "Admin";
                return RedirectToAction("Index", "Dashboard");
            }
            using (var db = new MydbContext()) {
                var USER = db.Users.FirstOrDefault(user => user.Password == pass && user.Email == email);
                if(USER != null)
                {
                   
                    Session["iduser"] = USER.Id; 
                    Session["usermail"] = USER.Email;
                    if (db.Hosts.Find(USER.Id) is null) {
                        Session["rule"] = "user";
                    }else
                    {
                        Session["rule"] = "host";
                    }

                }
            }
                

            return RedirectToAction("Index", "Home");
        }
    }
}