using System;
using System.Collections.Generic;
using System.Linq;
using AdvanceMockExample.Extensions;
using AdvanceMockExample.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvanceMockExample.Unit.Tests
{
    [TestClass]
    public class NotificationDataExtensionsTests
    {
        [TestMethod]
        public void WithNotificationDelayGtLastNotificationSentAtReturnsCorrectResult()
        {
            var sentAt = DateTime.UtcNow.AddMinutes(-5);
            var notification = new Notification { IsEnabled = true };
            var notificationRecipient1 = new NotificationRecipient{Delay = new TimeSpan(0,0,10),Notification = notification};
            var notificationEvent1 = new NotificationEvent {Notification = notification, Active = true};
            var notificationEventLog1 = new NotificationEventLog
            {
                NotificationEvent = notificationEvent1,
                NotificationRecipient = notificationRecipient1,
                SentAt = sentAt
            };
            var notificationRecipients = new List<NotificationRecipient>
            {
                notificationRecipient1
            };
            var notificationEvents = new List<NotificationEvent>
            {
                notificationEvent1
            };
            var notificationEventLogs = new List<NotificationEventLog>
            {
                notificationEventLog1
            };
            //Add collections
            notification.NotificationRecipients = notificationRecipients;
            notification.NotificationEvents = notificationEvents;
            notificationEvent1.NotificationEventLogs = notificationEventLogs;
            notificationRecipient1.NotificationEventLogs = notificationEventLogs;

            var result = notificationEvents.AsQueryable().WithNotificationDelayGtLastNotificationSentAt(DateTime.UtcNow).FirstOrDefault();
            Assert.IsNotNull(result);
        }
    }
}
