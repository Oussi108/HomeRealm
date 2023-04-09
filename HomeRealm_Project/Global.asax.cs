using HomeRealm_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HomeRealm_Project
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            using (var db = new MydbContext())
            {
                    DatabaseUpdater.UpdateDatabase();

                if (!db.Database.Exists())
                {
                    // create the database and run the migration
                    db.Database.Create();
                    db.Database.Initialize(true);
                }

                // check if the Person object already exists in the database
                if (!db.Users.Any(p => p.Email == "admin@admin.com"))
                {
                    // create a new Person object
                    var person = new User
                    {
                        FirstName = "admin",
                        LastName = "admin",
                        Email ="admin@admin.com",
                        Password = "admin",
                        PhoneNumber ="+212655555555"

                    };

                    // add the person to the People DbSet
                    db.Users.Add(person);

                    // save the changes to the database
                    db.SaveChanges();
                }
            }


        }
    }
}
