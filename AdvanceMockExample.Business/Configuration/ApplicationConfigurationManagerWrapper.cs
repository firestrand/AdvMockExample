using System;

namespace AdvanceMockExample.Business.Configuration
{
    public class ApplicationConfigurationManagerWrapper
    {
        public static Func<ApplicationConfigurationManager> Current = () => new ApplicationConfigurationManager();

        public static void Reset()
        {
            Current = () => new ApplicationConfigurationManager();
        }
    }
}