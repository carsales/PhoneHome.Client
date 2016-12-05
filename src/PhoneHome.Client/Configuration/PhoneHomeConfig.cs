using PhoneHome.Client.Enums;
using PhoneHome.Client.Interfaces;
using PhoneHome.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneHome.Client.Configuration
{

    public class PhoneHomeConfig : IPhoneHomeConfig
    {
        /// <summary>
        /// The name of the application that is phoning home
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// A unique key for your application
        /// </summary>
        public string ApplicationKey { get; set; }

        /// <summary>
        /// The version of your dll
        /// </summary>
        public string ApplicationVersion { get; set; }

        /// <summary>
        /// The environment that the application is running in (Development, Staging, UAT, Production)
        /// </summary>
        public string Environment { get; set; }

        /// <summary>
        /// How often should the phone home app check the health or your application
        /// </summary>
        public int? PhoneHomeFrequencySeconds { get; set; }

        /// <summary>
        /// Any extra meta data you might want to tag this application / server / environment with
        /// </summary>
        public IList<DataTag> Tags { get; set; }

        public Action<string, Exception> OnError { get; set; }

        public Guid? ApiKey { get; private set; }

        public string ApiHostUrl { get; private set; }

        public IList<IServerDataTagProvider> ServerDataTagProviders { get; internal set; }

        public IList<IApplicationInstanceDataTagProvider> ApplicationInstanceDataTagProviders { get; internal set; }

        public IList<IHealthcheckProvider> HealthcheckProviders { get; internal set; }

        public PhoneHomeConfig()
        {
            this.Tags = new List<DataTag>();
            this.ServerDataTagProviders = new List<IServerDataTagProvider>();
            this.ApplicationInstanceDataTagProviders = new List<IApplicationInstanceDataTagProvider>();
            this.HealthcheckProviders = new List<IHealthcheckProvider>();
        }

        public PhoneHomeConfig(Guid apiKey, string apiHostUrl) : this()
        {
            this.ApiKey = apiKey;
            this.ApiHostUrl = apiHostUrl;
        }
    }
}
