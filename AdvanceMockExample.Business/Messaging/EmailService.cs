using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using AdvanceMockExample.Business.Configuration;
using AdvanceMockExample.Models;

namespace AdvanceMockExample.Business.Messaging
{
    public class EmailService : IMessageService
    {
        private readonly DynamicConfiguration _configuration;
        public EmailService(DynamicConfiguration dynamicConfiguration)
        {
            _configuration = dynamicConfiguration;
        }
        public async Task SendAsync(Message message)
        {
            // Plug in your email service here to send an email.
            var destinationAddress = message.Destination;
            if (_configuration.OverrideEmailSendAddress) destinationAddress = _configuration.OverrideEmailSendAddressValue;

            var email =
               new MailMessage(new MailAddress("noreply@delta-techs.com", "(do not reply)"),
               new MailAddress(destinationAddress))
               {
                   Subject = message.Subject,
                   Body = message.Body,
                   IsBodyHtml = false
               };

            using (var client = new SmtpClient()) // SmtpClient configuration comes from config file
            {
                await client.SendMailAsync(email);
            }
        }
    }
}
