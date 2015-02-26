using System;
using System.Collections.Generic;
using System.Linq;
using AdvanceMockExample.Extensions;
using AdvanceMockExample.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvanceMockExample.Integration.Tests
{
    [TestClass]
    public class NotificationDataExtensionsTests
    {
        [TestMethod]
        public void WithNotificationDelayGtLastNotificationSentAtReturnsCorrectResult()
        {
            //Arrange
            var sentAt = DateTime.UtcNow.AddMinutes(-5);
            User user = new User{EmailAdress = "test@test.com"};
            Notification notification = new Notification{IsEnabled = true,Name = "Test Notification", Type = NotificationType.Warning};
            NotificationRecipient notificationRecipient = new NotificationRecipient{Delay = new TimeSpan(0,0,30),DeliveryMode = DeliveryMode.Email, Notification = notification,NotificationsPerEvent = 5, User = user};
            NotificationEvent notificationEvent = new NotificationEvent{Active = true, LastAlertTime = DateTime.UtcNow, Notification = notification,Value = "Test Failed"};
            NotificationEventLog notificationEventLog = new NotificationEventLog{NotificationEvent = notificationEvent,NotificationRecipient = notificationRecipient,SentAt = sentAt};
            NotificationEvent result;
            using (var context = new AppDbContext())
            {
                context.Users.Add(user);
                context.NotificationEventLogs.Add(notificationEventLog);
                context.SaveChanges();
            }
            //Act
            using (var context = new AppDbContext())
            {
                result = context.NotificationEvents.WithNotificationDelayGtLastNotificationSentAt(DateTime.UtcNow).FirstOrDefault();
            }

            using (var context = new AppDbContext())
            {
                context.NotificationEventLogs.Remove(notificationEventLog);
                context.NotificationEvents.Remove(notificationEvent);
                context.NotificationRecipients.Remove(notificationRecipient);
                context.Notifications.Remove(notification);
                context.Users.Remove(user);
                context.SaveChanges();
            }
            //Assert
            Assert.IsNotNull(result);
        }
    }
}
