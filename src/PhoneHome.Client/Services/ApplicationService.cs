using PhoneHome.Client.Configuration;
using PhoneHome.Client.Http;
using PhoneHome.Client.Interfaces;
using PhoneHome.Client.Models;
using fastJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneHome.Client.Web.Services
{
    internal interface IApplicationService
    {
        Application GetApplication(string applicationKey);
    }

    internal class ApplicationService : IApplicationService
    {
        private IPhoneHomeHttpClient _httpClient;
        private IPhoneHomeConfig _phoneHomeConfig;

        public ApplicationService(IPhoneHomeHttpClient httpClient, IPhoneHomeConfig phoneHomeConfig)
        {
            this._httpClient = httpClient;
            this._phoneHomeConfig = phoneHomeConfig;
        }

        public Application GetApplication(string applicationKey)
        {
            string url = string.Format("http://{0}/v1/applications?applicationKey={1}", this._phoneHomeConfig.ApiHostUrl, applicationKey);

            PhoneHomeHttpClientRequest request = PhoneHomeHttpClientRequest.Get(url);
            request.Headers.Add("Authorization", this._phoneHomeConfig.ApiKey.ToString());

            var response = this._httpClient.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JSON.ToObject<Application>(response.Body);
            }

            return null;
        }
    }
}
