using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneHome.Client.Models
{
    public class Application
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string HealthCheckUrl { get; set; }

        public int? PhonehomeFrequencySeconds { get; set; }
    }
}
