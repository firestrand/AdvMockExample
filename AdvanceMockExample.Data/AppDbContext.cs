using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvanceMockExample.Models;

namespace AdvanceMockExample
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DbContext") { }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<NotificationEvent> NotificationEvents { get; set; }
        public virtual DbSet<NotificationEventLog> NotificationEventLogs { get; set; }
        public virtual DbSet<NotificationRecipient> NotificationRecipients { get; set; }

        public virtual DbSet<ConfigurationSetting> ConfigurationSettings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotificationRecipient>()
                .HasRequired(p=>p.Notification)
                .WithMany(p=>p.NotificationRecipients)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<NotificationEvent>()
                .HasRequired(p=>p.Notification)
                .WithMany(p=>p.NotificationEvents)
                .WillCascadeOnDelete(false);
        }
    }
}
