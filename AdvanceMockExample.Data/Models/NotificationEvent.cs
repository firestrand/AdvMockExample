using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdvanceMockExample.Models
{
    public class NotificationEvent
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }
        public string Value { get; set; }
        public DateTime LastAlertTime { get; set; }
        public int NotificationId { get; set; }
        public virtual Notification Notification { get; set; }

        public virtual ICollection<NotificationEventLog> NotificationEventLogs { get; set; } 
    }
}