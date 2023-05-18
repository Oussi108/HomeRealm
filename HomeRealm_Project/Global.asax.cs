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
                        Picture = "Default_picture.jpg",
                        PhoneNumber ="+212655555555",
                        

                    };

                    // add the person to the People DbSet
                    db.Users.Add(person);

                    // save the changes to the database
                    AddUsers();
                    AddHosts();
                    AddProperties();
                    AddPropertyImages();
                    db.SaveChanges();
                    
                }
            }


        }

        public void AddUsers()
        {
            using (var db = new MydbContext()) // Replace 'YourDbContext' with your actual DbContext class
            {
                if (!db.Database.Exists())
                {
                    // create the database and run the migration
                    db.Database.Create();
                    db.Database.Initialize(true);
                }

                var usersToAdd = new List<User>();

                // Generate and add 10 users
                for (int i = 1; i <= 10; i++)
                {
                    var user = new User
                    {
                        FirstName = $"User{i}",
                        LastName = $"Last{i}",
                        Email = $"user{i}@example.com",
                        Password = $"password",
                        Picture = "Default_picture.jpg", // Set the picture value to "admin.jpg" for all users
                        PhoneNumber = $"+123456789{i}"
                    };

                    // check if the User object already exists in the database
                    if (!db.Users.Any(u => u.Email == user.Email))
                    {
                        // add the user to the Users DbSet
                        db.Users.Add(user);
                    }
                }

                // save the changes to the database
                db.SaveChanges();
            }
        }
        public void AddHosts()
        {
            using (var db = new MydbContext()) // Replace 'YourDbContext' with your actual DbContext class
            {
                var hostsToAdd = new List<Host>();

                for (int i = 1; i <= 5; i++)
                {
                    var host = new Host
                    {
                        User = new User
                        {
                            FirstName = $"Host{i}",
                            LastName = $"Last{i}",
                            Email = $"1host{i}@example.com",
                            Password = $"password{i}",
                            Picture = "admin.jpg",
                            PhoneNumber = $"+123456789{i}"
                        },
                        IDPicture = $"id_picture{i}.jpg",
                        Bio = $"Bio for Host{i}",
                        Birthday = new DateTime(1990, 1, 1).AddYears(i),
                        Country = "Country",
                        Verified = false
                    };

                    hostsToAdd.Add(host);
                }

                db.Hosts.AddRange(hostsToAdd);
                db.SaveChanges();
            }
        }
        public void AddProperties()
        {
            using (var db = new MydbContext()) // Replace 'YourDbContext' with your actual DbContext class
            {
                var propertiesToAdd = new List<Property>();

                for (int i = 1; i <= 5; i++)
                {
                    var property = new Property
                    {
                        Title = $"Property {i}",
                        Description = $"Property Description {i}",
                        Address = $"Property Address {i}",
                        City = $"Property City {i}",
                        Country = $"Property Country {i}",
                        ImageUrl = $"Default_prop.png",
                        Price = 100.00 + (i * 10), // Varying price for each property
                        Capacity = 4 + i, // Varying capacity for each property
                        IsAvailable = true,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        Host = new Host
                        {
                            User = new User
                            {
                                FirstName = $"Host{i}",
                                LastName = $"Last{i}",
                                Email = $"2host{i}@example.com",
                                Password = $"password{i}",
                                Picture = "Default_picture.jpg",
                                PhoneNumber = $"+123456789{i}"
                            },
                            IDPicture = $"id_picture{i}.jpg",
                            Bio = $"Host Bio {i}",
                            Birthday = new DateTime(1990, 1, 1).AddYears(i),
                            Country = $"Host Country {i}",
                            Verified = true
                        }
                    };

                    propertiesToAdd.Add(property);
                }

                db.Properties.AddRange(propertiesToAdd);
                db.SaveChanges();
            }
        }

        public void AddPropertyImages()
        {
            using (var db = new MydbContext()) // Replace 'YourDbContext' with your actual DbContext class
            {
                var propertyImagesToAdd = new List<PropertyImage>();

                // Get the properties from the database
                var properties = db.Properties.ToList();

                foreach (var property in properties)
                {
                    var propertyImage = new PropertyImage
                    {
                        PropertyId = property.Id,
                        DateUploaded = DateTime.Now,
                        ImageUrl = "Default_prop.png"
                    };

                    propertyImagesToAdd.Add(propertyImage);
                }

                db.PropertyImages.AddRange(propertyImagesToAdd);
                db.SaveChanges();
            }
        }


    }
}
