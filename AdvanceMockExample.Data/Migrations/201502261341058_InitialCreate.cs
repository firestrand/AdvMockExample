namespace AdvanceMockExample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConfigurationSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Server = c.String(maxLength: 20),
                        Key = c.Int(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NotificationEventLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SentAt = c.DateTime(nullable: false),
                        NotificationEventId = c.Int(nullable: false),
                        NotificationRecipientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NotificationRecipients", t => t.NotificationRecipientId, cascadeDelete: true)
                .ForeignKey("dbo.NotificationEvents", t => t.NotificationEventId, cascadeDelete: true)
                .Index(t => t.NotificationEventId)
                .Index(t => t.NotificationRecipientId);
            
            CreateTable(
                "dbo.NotificationEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Active = c.Boolean(nullable: false),
                        Value = c.String(),
                        LastAlertTime = c.DateTime(nullable: false),
                        NotificationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notifications", t => t.NotificationId)
                .Index(t => t.NotificationId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsEnabled = c.Boolean(nullable: false),
                        Type = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NotificationRecipients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Delay = c.Time(nullable: false, precision: 7),
                        DeliveryMode = c.Int(nullable: false),
                        NotificationsPerEvent = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        NotificationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notifications", t => t.NotificationId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.NotificationId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmailAdress = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotificationEventLogs", "NotificationEventId", "dbo.NotificationEvents");
            DropForeignKey("dbo.NotificationEvents", "NotificationId", "dbo.Notifications");
            DropForeignKey("dbo.NotificationRecipients", "UserId", "dbo.Users");
            DropForeignKey("dbo.NotificationEventLogs", "NotificationRecipientId", "dbo.NotificationRecipients");
            DropForeignKey("dbo.NotificationRecipients", "NotificationId", "dbo.Notifications");
            DropIndex("dbo.NotificationRecipients", new[] { "NotificationId" });
            DropIndex("dbo.NotificationRecipients", new[] { "UserId" });
            DropIndex("dbo.NotificationEvents", new[] { "NotificationId" });
            DropIndex("dbo.NotificationEventLogs", new[] { "NotificationRecipientId" });
            DropIndex("dbo.NotificationEventLogs", new[] { "NotificationEventId" });
            DropTable("dbo.Users");
            DropTable("dbo.NotificationRecipients");
            DropTable("dbo.Notifications");
            DropTable("dbo.NotificationEvents");
            DropTable("dbo.NotificationEventLogs");
            DropTable("dbo.ConfigurationSettings");
        }
    }
}
