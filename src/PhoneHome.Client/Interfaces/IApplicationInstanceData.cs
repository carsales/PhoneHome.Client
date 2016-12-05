using PhoneHome.Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneHome.Client.Interfaces
{
    public interface IApplicationInstanceData
    {
        /// <summary>
        /// The name of the application that is phoning home
        /// </summary>
        string ApplicationName { get; }

        /// <summary>
        /// Unique key inside phone home for your application
        /// </summary>
        string ApplicationKey { get; }

        /// <summary>
        /// The version of the application that is about to phone home
        /// </summary>
        string ApplicationVersion { get; }

        /// <summary>
        /// The environment that the application is running in (Development, Staging, UAT, Production)
        /// </summary>
        string Environment { get; }

        string PhoneHomeClientVersion { get; }

        IList<IApplicationHealth> Health { get; }

        IList<DataTag> Tags { get; set; }

        Server Server { get; set; }
    }
}
