using System;
using System.Configuration;
using System.Linq;
using AdvanceMockExample.Models;

namespace AdvanceMockExample.Business.Configuration
{
    public class DynamicConfiguration
    {
        private readonly AppDbContext _context;

        public DynamicConfiguration(AppDbContext context)
        {
            _context = context;
        }

        [SettingsDescription("Override the Email Send address for testing email")]
        public virtual bool OverrideEmailSendAddress
        {
            get
            {
                var setting =
                    _context.ConfigurationSettings.FirstOrDefault(
                        c => c.Key == Setting.OverrideEmailDestinationAddress && Server == this.Server);
                if (setting != null)
                {
                    return Convert.ToBoolean(setting.Value);
                }
                return false;
            }
        }

        [SettingsDescription("The Email Send address for testing email")]
        public virtual string OverrideEmailSendAddressValue
        {
            get
            {
                var setting =
                    _context.ConfigurationSettings.FirstOrDefault(
                        c => c.Key == Setting.OverrideEmailDestinationAddressValue && Server == this.Server);
                if (setting != null)
                {
                    return setting.Value;
                }
                //TODO: Set default value?
                return String.Empty;
            }
        }

        [SettingsDescription("Override the Sms Send number for testing")]
        public virtual bool OverrideSmsSendNumber
        {
            get
            {
                var setting =
                    _context.ConfigurationSettings.FirstOrDefault(
                        c => c.Key == Setting.OverrideSmsDestinationNumber && Server == this.Server);
                if (setting != null)
                {
                    return Convert.ToBoolean(setting.Value);
                }
                return false;
            }
        }

        [SettingsDescription("The Sms number value for testing")]
        public virtual string OverrideSmsSendNumberValue
        {
            get
            {
                var setting =
                    _context.ConfigurationSettings.FirstOrDefault(
                        c => c.Key == Setting.OverrideSmsDestinationNumberValue && Server == this.Server);
                if (setting != null)
                {
                    return setting.Value;
                }
                //TODO: Set default value?
                return String.Empty;
            }
        }

        [SettingsDescription("The Sms Account Sid")]
        public virtual string SmsAccountSid
        {
            get
            {
                var setting =
                    _context.ConfigurationSettings.FirstOrDefault(
                        c => c.Key == Setting.SmsAccountSid && Server == this.Server);
                if (setting != null)
                {
                    return setting.Value;
                }
                //TODO: Set default value?
                return String.Empty;
            }
        }

        [SettingsDescription("The Sms Account Auth Token")]
        public virtual string SmsAuthToken
        {
            get
            {
                var setting =
                    _context.ConfigurationSettings.FirstOrDefault(
                        c => c.Key == Setting.SmsAuthToken && Server == this.Server);
                if (setting != null)
                {
                    return setting.Value;
                }
                //TODO: Set default value?
                return String.Empty;
            }
        }

        [SettingsDescription("The Sms from number")]
        public virtual string SmsFromNumber
        {
            get
            {
                var setting =
                    _context.ConfigurationSettings.FirstOrDefault(
                        c => c.Key == Setting.SmsFromNumber && Server == this.Server);
                if (setting != null)
                {
                    return setting.Value;
                }
                //TODO: Set default value?
                return String.Empty;
            }
        }

        [SettingsDescription("Gets the Server configuration value")]
        public virtual string Server
        {
            get { return ApplicationConfigurationManagerWrapper.Current().AppSettings["Server"]; }
        }
    }
}
