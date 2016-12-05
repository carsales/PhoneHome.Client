using PhoneHome.Client.Http;
using PhoneHome.Client.Timers;
using PhoneHome.Client.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyIoC;

namespace PhoneHome.Client.Ioc
{
    public class Bindings
    {
        public static TinyIoCContainer Register()
        {
            TinyIoCContainer container = TinyIoCContainer.Current;

            container.Register<IPhoneHomeService, PhoneHomeService>();
            container.Register<IPhoneHomeTimer, PhoneHomeTimer>();
            container.Register<IApplicationService, ApplicationService>();

            container.Register<IPhoneHomeHttpClient, PhoneHomeHttpClient>();

            return container;
        }
    }
}
