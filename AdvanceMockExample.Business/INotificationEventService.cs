using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AdvanceMockExample.Models;

namespace AdvanceMockExample.Business
{
    public interface INotificationEventService
    {
        IEnumerable<NotificationEvent> GetActiveSignalingNotificationEvents(DateTime signalTime);
        IEnumerable<NotificationRecipient> GetActiveSignallingRecipientsForNotificationEvent(NotificationEvent notificationEvent, DateTime signalTime);
    }
}
