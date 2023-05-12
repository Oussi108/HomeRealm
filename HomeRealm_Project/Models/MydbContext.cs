using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace HomeRealm_Project.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]

    public class MydbContext : DbContext
    {
        public MydbContext() : base("Mycon")
        {

            Database.SetInitializer(new CreateDatabaseIfNotExists<MydbContext>());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Host> Hosts { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure foreign keys
            modelBuilder.Entity<Booking>()
                .HasRequired(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Booking>()
                .HasRequired(b => b.Property)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.PropertyId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasRequired(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasRequired(m => m.Receiver)
                .WithMany()
                .HasForeignKey(m => m.ReceiverId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Review>()
                .HasRequired(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Review>()
                .HasRequired(r => r.Property)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.PropertyId)
                .WillCascadeOnDelete(false);
        }

    }
    public static class DatabaseUpdater
    {
        public static void UpdateDatabase()
        {
            using (var db = new MydbContext())
            {
                // check if the database needs to be updated
                if (db.Database.CompatibleWithModel(false))
                {
                    // update the database
                    var migrator = new DbMigrator(new MyDbConfiguration());
                    migrator.Update();
                }
            }
        }
    }
    public class MyDbConfiguration : DbMigrationsConfiguration<MydbContext>
    {
        public MyDbConfiguration()
        {
          
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}