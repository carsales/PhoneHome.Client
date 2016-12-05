using PhoneHome.Client.Configuration;
using PhoneHome.Client.Enums;
using PhoneHome.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PhoneHome.Client.Models
{
    public class ApplicationInstanceData : IApplicationInstanceData
    {
        /// <summary>
        /// The name of the application that is phoning home
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Unique key inside phone home for your application
        /// </summary>
        public string ApplicationKey { get; set; }

        /// <summary>
        /// The version of the application that is about to phone home
        /// </summary>
        public string ApplicationVersion { get; set; }

        /// <summary>
        /// All the server details
        /// </summary>
        public Server Server { get; set; }

        /// <summary>
        /// Gets the cpu usage of this instance
        /// </summary>
        public decimal? CPU { get; set; }

        /// <summary>
        /// Gets the memory usage of this instance
        /// </summary>
        public decimal? MemoryUsed { get; set; }

        /// <summary>
        /// The environment that the application is running in (Development, Staging, UAT, Production)
        /// </summary>
        public string Environment { get; set; }

        public string PhoneHomeClientVersion { get; private set; }

        public IList<IApplicationHealth> Health { get; set; }

        public IList<DataTag> Tags { get; set; }

        public ApplicationInstanceData(IPhoneHomeConfig config)
        {
            this.ApplicationName = config.ApplicationName;
            this.ApplicationKey = config.ApplicationKey;
            this.ApplicationVersion = config.ApplicationVersion;
            this.Environment = config.Environment;
            this.PhoneHomeClientVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            this.Server = new Server(config);

            this.Health = new List<IApplicationHealth>();
            this.Tags = new List<DataTag>();

            foreach(var tag in config.Tags)
            {
                this.Tags.Add(tag);
            }
        }
    }
}
