using PhoneHome.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneHome.Client.Interfaces
{
    public interface IApplicationInstanceDataTagProvider
    {
        IList<DataTag> GetTags();
    }
}
