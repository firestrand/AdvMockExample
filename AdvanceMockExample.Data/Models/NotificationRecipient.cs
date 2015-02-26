using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AdvanceMockExample.Models
{
    public class NotificationRecipient
    {
        [Key]
        public int Id { get; set; }
        public TimeSpan Delay { get; set; }
        public DeliveryMode DeliveryMode { get; set; }
        public int NotificationsPerEvent { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int NotificationId { get; set; }
        public virtual Notification Notification { get; set; }
        
        public virtual ICollection<NotificationEventLog> NotificationEventLogs { get; set; } 

    }
}