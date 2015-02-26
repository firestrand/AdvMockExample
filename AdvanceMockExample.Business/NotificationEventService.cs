using System;
using System.Collections.Generic;
using System.Linq;
using AdvanceMockExample.Extensions;
using AdvanceMockExample.Models;

namespace AdvanceMockExample.Business
{
    public class NotificationEventService : INotificationEventService
    {
        private readonly AppDbContext _context;
        public NotificationEventService(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<NotificationEvent> GetActiveSignalingNotificationEvents(DateTime signalTime)
        {
            return _context.NotificationEvents.WithEnabledNotification().AreActive().WithNotificationsPerEventNotExceeded().WithNotificationDelayGtLastNotificationSentAt(signalTime);
        }

        public IEnumerable<NotificationRecipient> GetActiveSignallingRecipientsForNotificationEvent(NotificationEvent notificationEvent, DateTime signalTime)
        {
            return notificationEvent.Notification.NotificationRecipients.AsQueryable().WithNotificationsPerEventNotExceeded().WithNotificationDelayGtLastNotificationSentAt(signalTime);
        }
    }
}