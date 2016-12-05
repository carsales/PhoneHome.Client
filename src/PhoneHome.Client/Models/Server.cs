using PhoneHome.Client.Constants;
using PhoneHome.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PhoneHome.Client.Models
{
    public class Server
    {        
        /// <summary>
        /// The name of the server this application is running on (Will get it from HttpContext.Current.Request
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The active mac address for this server
        /// </summary>
        public string MacAddress { get; set; }

        /// <summary>
        /// The IP address of the server that is phoning home
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets the cpu usage of this instance
        /// </summary>
        public decimal? CPU { get; set; }

        /// <summary>
        /// Gets the total used memory usage on this server
        /// </summary>
        public decimal? MemoryUsed { get; set; }

        /// <summary>
        /// Gets the total memory on this server
        /// </summary>
        public decimal? MemoryTotal { get; set; }

        public IList<DataTag> Tags { get; set; }

        public Server(IPhoneHomeConfig config)
        {
            this.IpAddress = this.GetIpAddress();
            this.Name = System.Environment.MachineName;
            this.MacAddress = this.GetNetworkInterfaces().Select(nic => nic.GetPhysicalAddress().ToString()).FirstOrDefault();

            this.Tags = new List<DataTag>();
        }

        public string GetIpAddress()
        {
            try
            {
                foreach (var ni in this.GetNetworkInterfaces())
                {
                    foreach (var uipi in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (uipi.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            if (uipi.IPv4Mask != null)
                            {
                                return uipi.Address.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (PhoneHomeClient.OnError != null)
                {
                    PhoneHomeClient.OnError(PhoneHomeErrorType.IpAddress, e);
                }
            }

            return null;
        }

        private IEnumerable<NetworkInterface> GetNetworkInterfaces()
        {
            return NetworkInterface.GetAllNetworkInterfaces().Where(nic => nic.OperationalStatus == OperationalStatus.Up);
        }
    }
}
