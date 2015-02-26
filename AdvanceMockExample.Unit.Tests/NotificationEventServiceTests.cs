using System;
using System.Collections.Generic;
using System.Data.Entity;
using AdvanceMockExample.Business;
using AdvanceMockExample.Business.Messaging;
using AdvanceMockExample.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AdvanceMockExample.Unit.Tests
{
    [TestClass]
    public class NotificationEventServiceTests
    {
        [TestMethod]
        public void SendSensorNotificationsWithActiveNotifyingEventSensEmail()
        {
            var lastAlertTime = DateTime.UtcNow.AddMinutes(-5);
            var notification = new Notification{IsEnabled = true, Type = NotificationType.Warning,Name = "Test Notification"};
            var notificationEvent = new NotificationEvent{LastAlertTime = lastAlertTime, Notification = notification, Active = true, Value = "Alert 1"};
            var user = new User { EmailAdress = "test@fake.com" };
            var recipient = new NotificationRecipient{User = user, Delay = new TimeSpan(0,0,30), DeliveryMode = DeliveryMode.Email,Notification = notification};

            //Setup Mock for Add
            var mockSet = new Mock<DbSet<NotificationEventLog>>();
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.NotificationEventLogs).Returns(mockSet.Object);

            var mockEmailService = new Mock<IMessageService>();

            var mockNotificationEventService = new Mock<INotificationEventService>();
            mockNotificationEventService.Setup(m => m.GetActiveSignalingNotificationEvents(It.IsAny<DateTime>())).Returns(new List<NotificationEvent> { notificationEvent });
            mockNotificationEventService.Setup(m => m.GetActiveSignallingRecipientsForNotificationEvent(notificationEvent, It.IsAny<DateTime>())).Returns(new List<NotificationRecipient> { recipient });

            //Act
            var sensorNotificationService = new NotificationService(mockContext.Object, mockNotificationEventService.Object, mockEmailService.Object, null);
            sensorNotificationService.SendNotifications(DateTime.UtcNow);

            //Assert
            mockSet.Verify(m => m.Add(It.IsAny<NotificationEventLog>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
            mockEmailService.Verify(m => m.SendAsync(It.IsAny<Message>()), Times.Once);
        }
    }
}
