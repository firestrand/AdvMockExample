using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceMockExample.Models
{
    public enum Setting
    {
        None = 0,
        OverrideEmailDestinationAddress,
        OverrideEmailDestinationAddressValue,
        OverrideSmsDestinationNumber,
        OverrideSmsDestinationNumberValue,
        SmsAccountSid,
        SmsAuthToken,
        SmsFromNumber
    }

    public class ConfigurationSetting
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string Server { get; set; }

        public Setting Key { get; set; }
        public string Value { get; set; }
    }
}
