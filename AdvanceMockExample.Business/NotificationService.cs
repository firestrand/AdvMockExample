using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvanceMockExample.Business.Messaging;
using AdvanceMockExample.Extensions;
using AdvanceMockExample.Models;

namespace AdvanceMockExample.Business
{
    public class NotificationService
    {
        private readonly AppDbContext _context;
        private readonly IMessageService _emailService;
        private readonly IMessageService _smsService;
        //private readonly INotificationEventService _notificationEventService;

        public NotificationService(AppDbContext context, INotificationEventService notificationEventService,
            IMessageService emailService, IMessageService smsService)
        {
            _context = context;
            _emailService = emailService;
            _smsService = smsService;
            //_notificationEventService = notificationEventService;
        }

        public virtual async void SendNotifications(DateTime signalTime)
        {
            //var events = _notificationEventService.GetActiveSignalingNotificationEvents(signalTime);
            var events = _context.NotificationEvents.WithEnabledNotification().AreActive().WithNotificationsPerEventNotExceeded().WithNotificationDelayGtLastNotificationSentAt(signalTime);
            foreach (var notificationEvent in events)
            {
                //var recipients = _notificationEventService.GetActiveSignallingRecipientsForNotificationEvent(sensorNotificationEvent, signalTime);
                var recipients = notificationEvent.Notification.NotificationRecipients.AsQueryable().WithNotificationsPerEventNotExceeded().WithNotificationDelayGtLastNotificationSentAt(signalTime);
                foreach (var recipient in recipients)
                {
                    if (recipient.DeliveryMode != DeliveryMode.None)
                    {
                        var subject = String.Format("{0}:{1}", recipient.Notification.Type, recipient.Notification.Name);
                        var body = String.Format("{0}:{1}: {2} has generated this message. {3}",
                            recipient.Notification.Type,
                            recipient.Notification.Name,
                            notificationEvent.Value,
                            notificationEvent.LastAlertTime);

                        if (recipient.DeliveryMode == DeliveryMode.Email)
                        {
                            var message = new Message
                            {
                                Subject = subject,
                                Body = body,
                                Destination = recipient.User.EmailAdress
                            };
                            await _emailService.SendAsync(message);
                        }
                        if (recipient.DeliveryMode == DeliveryMode.Sms)
                        {
                            var message = new Message {Body = body, Destination = recipient.User.PhoneNumber};
                            await _smsService.SendAsync(message);
                        }

                        _context.NotificationEventLogs.Add(new NotificationEventLog
                        {
                            NotificationEventId = notificationEvent.Id,
                            NotificationRecipientId = recipient.Id,
                            SentAt = DateTime.UtcNow
                        });
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }

        public virtual IEnumerable<NotificationEvent> GetEventsForNotification(int notificationId, DateTime start, DateTime end)
        {
            return _context.NotificationEvents.Where(ne => ne.NotificationId == notificationId && ne.LastAlertTime > start && ne.LastAlertTime < end);
        } 
    }
}
