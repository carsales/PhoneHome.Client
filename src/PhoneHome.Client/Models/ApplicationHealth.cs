using PhoneHome.Client.Enums;
using PhoneHome.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneHome.Client.Models
{
    public class ApplicationHealth : IApplicationHealth
    {
        public ApplicationHealthResult Result { get; set; }

        public IList<DataTag> Tags { get; set; }

        private ApplicationHealth()
        {
            this.Tags = new List<DataTag>();
        }

        public static ApplicationHealth Healthy()
        {
            return new ApplicationHealth() { Result = ApplicationHealthResult.Healthy };
        }

        public static ApplicationHealth Unhealthy()
        {
            return new ApplicationHealth() { Result = ApplicationHealthResult.Unhealthy };
        }
    }
}