using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdvanceMockExample.Models
{
    public enum NotificationType
    {
        Unknown,
        Warning,
        Error
    }
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public bool IsEnabled { get; set; }
        public NotificationType Type { get; set; }
        public string Name { get; set; }
        public virtual ICollection<NotificationEvent> NotificationEvents { get; set; }
        public virtual ICollection<NotificationRecipient> NotificationRecipients { get; set; } 

    }
}