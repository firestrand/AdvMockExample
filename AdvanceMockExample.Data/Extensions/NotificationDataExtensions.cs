using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvanceMockExample.Models;

namespace AdvanceMockExample.Extensions
{
    public static class NotificationDataExtensions
    {
        public static IQueryable<NotificationRecipient> WithNotificationsPerEventNotExceeded(this IQueryable<NotificationRecipient> baseQuery)
        {
            return baseQuery.Where(nr => nr.NotificationEventLogs.Count < nr.NotificationsPerEvent);
        }
        public static IQueryable<NotificationRecipient> WithNotificationDelayGtLastNotificationSentAt(this IQueryable<NotificationRecipient> baseQuery, DateTime signalTime)
        {
            return baseQuery.Where(nr => nr.Delay < signalTime - nr.NotificationEventLogs.Max(sner => sner.SentAt));
        }
        //public static IQueryable<NotificationRecipient> WithNotificationDelayGtLastNotificationSentAt(this IQueryable<NotificationRecipient> baseQuery, DateTime signalTime)
        //{
        //    return baseQuery.Where(nr => nr.DelaySeconds < DbFunctions.DiffSeconds(nr.NotificationEventLogs.Max(sner => sner.SentAt),signalTime));
        //}
        public static IQueryable<NotificationEvent> WithEnabledNotification(this IQueryable<NotificationEvent> baseQuery)
        {
            return baseQuery.Where(ne => ne.Notification.IsEnabled);
        }
        public static IQueryable<NotificationEvent> AreActive(this IQueryable<NotificationEvent> baseQuery)
        {
            return baseQuery.Where(ne => ne.Active);
        }
        public static IQueryable<NotificationEvent> WithNotificationsPerEventNotExceeded(this IQueryable<NotificationEvent> baseQuery)
        {
            return baseQuery.Where(ne => ne.Notification.NotificationRecipients.Any(nr=>nr.NotificationEventLogs.Count < nr.NotificationsPerEvent));
        } 
        public static IQueryable<NotificationEvent> WithNotificationDelayGtLastNotificationSentAt(this IQueryable<NotificationEvent> baseQuery, DateTime signalTime)
        {
            return baseQuery.Where(sne => sne.Notification.NotificationRecipients.Any(snr => snr.Delay < signalTime - snr.NotificationEventLogs.Max(sner => sner.SentAt)));
        }
        //public static IQueryable<NotificationEvent> WithNotificationDelayGtLastNotificationSentAt(this IQueryable<NotificationEvent> baseQuery, DateTime signalTime)
        //{
        //    return baseQuery.Where(sne => sne.Notification.NotificationRecipients.Any(snr => snr.DelaySeconds < DbFunctions.DiffSeconds(snr.NotificationEventLogs.Max(sner => sner.SentAt), signalTime)));
        //}
    }
}
