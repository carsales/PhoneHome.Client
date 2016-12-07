using PhoneHome.Client.Constants;
using PhoneHome.Client.Interfaces;
using PhoneHome.Client.Models;
using PhoneHome.Client.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PhoneHome.Client.Timers
{
    internal interface IPhoneHomeTimer
    {
        void Start();
    }

    internal class PhoneHomeTimer : IPhoneHomeTimer
    {
        private IApplicationService _applicationService;
        private IPhoneHomeService _phoneHomeService;
        private IPhoneHomeConfig _phoneHomeConfig;

        private static Timer _phonehomeTimer;


        public PhoneHomeTimer(IApplicationService applicationService, IPhoneHomeService phoneHomeService, IPhoneHomeConfig phoneHomeConfig)
        {
            this._applicationService = applicationService;
            this._phoneHomeService = phoneHomeService;
            this._phoneHomeConfig = phoneHomeConfig;
        }


        public void Start()
        {
            this.CreateTimer(1);
        }


        public void CreateTimer(int delay)
        {
            try
            {
                _phonehomeTimer = new Timer(delay);
                _phonehomeTimer.Elapsed += PhonehomeTimerElapsed;
                _phonehomeTimer.AutoReset = false;
                _phonehomeTimer.Start();
            }
            catch (Exception exception)
            {
                if (PhoneHomeClient.OnError != null)
                {
                    PhoneHomeClient.OnError(PhoneHomeErrorType.AppStart, exception);
                }
            }
        }

        private int GetPhonehomeFrequency()
        {
            int? phoneHomeFrequency = null;

            // get the application from the api to find out how often we should phone home
            Application application = this._applicationService.GetApplication(this._phoneHomeConfig.ApplicationKey);

            // Try to get the phone home frequency from the saved application
            if (application != null)
            {
                phoneHomeFrequency = application.PhonehomeFrequencySeconds;
            }

            if (!phoneHomeFrequency.HasValue)
            {
                phoneHomeFrequency = (this._phoneHomeConfig.PhoneHomeFrequencySeconds);
            }

            // still no value then default to 5 minutes!
            if (!phoneHomeFrequency.HasValue)
            {
                phoneHomeFrequency = 1000 * 60 * 5;
            }

            return phoneHomeFrequency.Value;
        }


        private void PhonehomeTimerElapsed(Object source, ElapsedEventArgs e)
        {
            try
            {
                this._phoneHomeService.Execute();
            }
            catch (Exception exception)
            {
                if (PhoneHomeClient.OnError != null)
                {
                    PhoneHomeClient.OnError(PhoneHomeErrorType.PhoneHome, exception);
                }
            }
            finally
            {
                // Restart the timer
                this.CreateTimer(GetPhonehomeFrequency());
            }
        }
    }
}
