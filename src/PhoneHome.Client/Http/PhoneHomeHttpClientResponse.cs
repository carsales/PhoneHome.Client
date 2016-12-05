using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PhoneHome.Client.Http
{
    public class PhoneHomeHttpClientResponse
    {
        public HttpStatusCode? StatusCode { get; private set; }

        public string Body { get; set; }

        internal PhoneHomeHttpClientResponse() { }

        internal PhoneHomeHttpClientResponse(HttpWebResponse httpWebResponse)
        {
            if (httpWebResponse == null)
            {
                return;
            }

            this.StatusCode = httpWebResponse.StatusCode;

            using (var rs = httpWebResponse.GetResponseStream())
            {
                if (rs != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        rs.CopyTo(ms);

                        this.Body = Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
        }
    }
}
