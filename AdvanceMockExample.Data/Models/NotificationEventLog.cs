using System;
using System.ComponentModel.DataAnnotations;

namespace AdvanceMockExample.Models
{
    public class NotificationEventLog
    {
        [Key]
        public int Id { get; set; }

        public DateTime SentAt { get; set; }

        public int NotificationEventId { get; set; }
        public virtual NotificationEvent NotificationEvent { get; set; }

        public int NotificationRecipientId { get; set; }
        public virtual NotificationRecipient NotificationRecipient { get; set; }

        
        
    }
}