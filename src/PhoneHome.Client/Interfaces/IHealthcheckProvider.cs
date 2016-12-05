using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneHome.Client.Interfaces
{
    public interface IHealthcheckProvider
    {
        IApplicationHealth GetHealth(IApplicationInstanceData data);
    }
}
