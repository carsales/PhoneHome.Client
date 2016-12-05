using PhoneHome.Client.Interfaces;
using PhoneHome.Client.Ioc;
using PhoneHome.Client.Timers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using TinyIoC;
using PhoneHome.Client.Constants;

namespace PhoneHome.Client
{
    public class PhoneHomeClient
    {
        // call back func if there is an error inside the phone home client
        public static Action<string, Exception> OnError = null;

        private static TinyIoCContainer Container = null;

        static PhoneHomeClient()
        {
            PhoneHomeClient.Container = Bindings.Register();
        }

        public static void Register(IPhoneHomeConfig config)
        {
            try
            {
                PhoneHomeClient.OnError = config.OnError;

                if (!config.ApiKey.HasValue)
                {
                    PhoneHomeClient.OnError(PhoneHomeErrorType.ApiConfig, new Exception("No Api Key Provided"));
                }

                if (string.IsNullOrEmpty(config.ApiHostUrl))
                {
                    PhoneHomeClient.OnError(PhoneHomeErrorType.ApiConfig, new Exception("No Api Host Url Provided"));
                }

                if(string.IsNullOrEmpty(config.ApplicationKey))
                {
                    PhoneHomeClient.OnError(PhoneHomeErrorType.ApiConfig, new Exception("No Application Key Provided. Please give your application a unique key for your organisation"));
                }

                PhoneHomeClient.Container.Register<IPhoneHomeConfig>(config);
                PhoneHomeClient.Container.RegisterMultiple<IServerDataTagProvider>(config.ServerDataTagProviders.Select(o => o.GetType()));
                PhoneHomeClient.Container.RegisterMultiple<IApplicationInstanceDataTagProvider>(config.ApplicationInstanceDataTagProviders.Select(o => o.GetType()));
                PhoneHomeClient.Container.RegisterMultiple<IHealthcheckProvider>(config.HealthcheckProviders.Select(o => o.GetType()));

                PhoneHomeClient.Container.Resolve<IPhoneHomeTimer>().Start();
            }
            catch (Exception exception)
            {
                if (PhoneHomeClient.OnError != null)
                {
                    PhoneHomeClient.OnError(PhoneHomeErrorType.AppStart, exception);
                }
            }
        }
    }
}

