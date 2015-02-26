using System.Threading.Tasks;
using AdvanceMockExample.Business.Configuration;
using Twilio;
using Message = AdvanceMockExample.Models.Message;

namespace AdvanceMockExample.Business.Messaging
{
    public class SmsService : IMessageService
    {
        private readonly DynamicConfiguration _configuration;
        public SmsService(DynamicConfiguration dynamicConfiguration)
        {
            _configuration = dynamicConfiguration;
        }
        public Task SendAsync(Message message)
        {
            string accountSid = _configuration.SmsAccountSid;
            string authToken = _configuration.SmsAuthToken;
            string fromNumber = _configuration.SmsFromNumber;
            string destinationNumber = message.Destination;
            if (_configuration.OverrideSmsSendNumber)
            {
                destinationNumber = _configuration.OverrideSmsSendNumberValue;
            }

            // instantiate a new Twilio Rest Client
            var client = new TwilioRestClient(accountSid, authToken);
            client.SendMessage(fromNumber, destinationNumber, message.Body);
            return Task.FromResult(0);
        }
    }
}
