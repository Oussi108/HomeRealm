namespace HomeRealm_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Realm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        CheckInDate = c.DateTime(nullable: false, precision: 0),
                        CheckOutDate = c.DateTime(nullable: false, precision: 0),
                        PropertyId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Property", t => t.PropertyId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.PropertyId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Property",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Description = c.String(nullable: false, unicode: false),
                        Address = c.String(nullable: false, unicode: false),
                        City = c.String(nullable: false, unicode: false),
                        Country = c.String(nullable: false, unicode: false),
                        ImageUrl = c.String(nullable: false, unicode: false),
                        Price = c.Double(nullable: false),
                        Capacity = c.Int(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        UpdatedAt = c.DateTime(nullable: false, precision: 0),
                        HostId = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Host", t => t.HostId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.HostId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Host",
                c => new
                    {
                        HostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HostId)
                .ForeignKey("dbo.Users", t => t.HostId)
                .Index(t => t.HostId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        LastName = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Email = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        Password = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        PhoneNumber = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.review",
                c => new
                    {
                        review_id = c.Int(nullable: false, identity: true),
                        content = c.String(nullable: false, unicode: false),
                        rating = c.Int(nullable: false),
                        property_id = c.Int(nullable: false),
                        user_id = c.Int(nullable: false),
                        date_created = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.review_id)
                .ForeignKey("dbo.Property", t => t.property_id)
                .ForeignKey("dbo.Users", t => t.user_id)
                .Index(t => t.property_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.message",
                c => new
                    {
                        message_id = c.Int(nullable: false, identity: true),
                        content = c.String(nullable: false, unicode: false),
                        sender_id = c.Int(nullable: false),
                        receiver_id = c.Int(nullable: false),
                        sent_time = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.message_id)
                .ForeignKey("dbo.Users", t => t.receiver_id)
                .ForeignKey("dbo.Users", t => t.sender_id)
                .Index(t => t.sender_id)
                .Index(t => t.receiver_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.message", "sender_id", "dbo.Users");
            DropForeignKey("dbo.message", "receiver_id", "dbo.Users");
            DropForeignKey("dbo.Bookings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Bookings", "PropertyId", "dbo.Property");
            DropForeignKey("dbo.Host", "HostId", "dbo.Users");
            DropForeignKey("dbo.review", "user_id", "dbo.Users");
            DropForeignKey("dbo.review", "property_id", "dbo.Property");
            DropForeignKey("dbo.Property", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Property", "HostId", "dbo.Host");
            DropIndex("dbo.message", new[] { "receiver_id" });
            DropIndex("dbo.message", new[] { "sender_id" });
            DropIndex("dbo.review", new[] { "user_id" });
            DropIndex("dbo.review", new[] { "property_id" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Host", new[] { "HostId" });
            DropIndex("dbo.Property", new[] { "User_Id" });
            DropIndex("dbo.Property", new[] { "HostId" });
            DropIndex("dbo.Bookings", new[] { "UserId" });
            DropIndex("dbo.Bookings", new[] { "PropertyId" });
            DropTable("dbo.message");
            DropTable("dbo.review");
            DropTable("dbo.Users");
            DropTable("dbo.Host");
            DropTable("dbo.Property");
            DropTable("dbo.Bookings");
        }
    }
}
