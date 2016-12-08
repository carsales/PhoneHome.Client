using PhoneHome.Client.Enums;
using PhoneHome.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneHome.Client.Interfaces
{
    public interface IPhoneHomeConfig
    {
        /// <summary>
        /// The name of the application that is phoning home
        /// </summary>
        string ApplicationName { get; }

        /// <summary>
        /// A unique key for your application
        /// </summary>
        string ApplicationKey { get; }

        /// <summary>
        /// The version of your dll
        /// </summary>
        string ApplicationVersion { get; }

        /// <summary>
        /// The type of application that is phoning home (website, api, windows service etc)
        /// </summary>
        string ApplicationType { get; }

        /// <summary>
        /// The environment that the application is running in (Development, Staging, UAT, Production)
        /// </summary>
        string Environment { get; }

        /// <summary>
        /// How often should the phone home app check the health or your application
        /// </summary>
        int? PhoneHomeFrequencySeconds { get; }

        /// <summary>
        /// Any extra meta data you might want to tag this application / server / environment with
        /// </summary>
        IList<DataTag> Tags { get; }

        Action<string, Exception> OnError { get; }

        Guid? ApiKey { get; }

        string ApiHostUrl { get; }

        /// <summary>
        /// Ability to register any custom server data tag providers
        /// </summary>
        IList<IServerDataTagProvider> ServerDataTagProviders { get; }

        /// <summary>
        /// Ability to register any custom application instance data tag providers
        /// </summary>
        IList<IApplicationInstanceDataTagProvider> ApplicationInstanceDataTagProviders { get; }

        /// <summary>
        /// Ability to register any custom health check providers
        /// </summary>
        IList<IHealthcheckProvider> HealthcheckProviders { get; }
    }
}
