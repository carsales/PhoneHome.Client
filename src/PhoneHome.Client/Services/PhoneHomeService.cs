using PhoneHome.Client.Configuration;
using PhoneHome.Client.Constants;
using PhoneHome.Client.Enums;
using PhoneHome.Client.Http;
using PhoneHome.Client.Interfaces;
using PhoneHome.Client.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PhoneHome.Client.Web.Services
{
    internal interface IPhoneHomeService
    {
        void Execute();
    }

    internal class PhoneHomeService : IPhoneHomeService
    {
        private IPhoneHomeHttpClient _httpClient;
        private IPhoneHomeConfig _phoneHomeConfig;

        public PhoneHomeService(IPhoneHomeHttpClient httpClient, IPhoneHomeConfig phoneHomeConfig)
        {
            this._httpClient = httpClient;
            this._phoneHomeConfig = phoneHomeConfig;
        }

        public void Execute()
        {
            try
            {
                IApplicationInstanceData applicationData = this.GetApplicationData();

                string url = string.Format("http://{0}/v2/phone-home", this._phoneHomeConfig.ApiHostUrl);

                PhoneHomeHttpClientRequest request = PhoneHomeHttpClientRequest.Put(url, fastJSON.JSON.ToJSON(applicationData));
                request.Headers.Add("Authorization", this._phoneHomeConfig.ApiKey.ToString());

                var response = this._httpClient.Execute(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception("unable to phone home: " + url + ", StatusCode: " + response.StatusCode.ToString());
                }
                
            }
            catch (Exception e)
            {
                if (PhoneHomeClient.OnError != null)
                {
                    PhoneHomeClient.OnError(PhoneHomeErrorType.PhoneHome, e);
                }
            }
        }

        private IApplicationInstanceData GetApplicationData()
        {
            IApplicationInstanceData applicationData = new ApplicationInstanceData(this._phoneHomeConfig);

            foreach (var result in this._phoneHomeConfig.HealthcheckProviders.Select(o => o.GetHealth(applicationData)).Where(o => o != null))
            {
                applicationData.Health.Add(result);
            }

            applicationData.Tags = new List<DataTag>();

            foreach (var tag in this._phoneHomeConfig.ApplicationInstanceDataTagProviders.SelectMany(o => o.GetTags()).Where(o => o.Value != null))
            {
                applicationData.Tags.Add(tag);
            }


            applicationData.Server.Tags = new List<DataTag>();

            foreach (var tag in this._phoneHomeConfig.ServerDataTagProviders.SelectMany(o => o.GetTags()).Where(o => o.Value != null))
            {
                applicationData.Server.Tags.Add(tag);
            }

            return applicationData;
        }
    }
}