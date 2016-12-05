using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PhoneHome.Client.Http
{
    public interface IPhoneHomeHttpClient
    {
        PhoneHomeHttpClientResponse Execute(PhoneHomeHttpClientRequest request);
    }

    public class PhoneHomeHttpClient : IPhoneHomeHttpClient
    {

        public PhoneHomeHttpClient()
        {
        }

        public PhoneHomeHttpClientResponse Execute(PhoneHomeHttpClientRequest request)
        {
            HttpWebRequest httpWebRequest = this.GetHttpWebRequest(request);
            PhoneHomeHttpClientResponse response = null;

            try
            {
                HttpWebResponse httpWebResponse = this.Sync(httpWebRequest);

                response = new PhoneHomeHttpClientResponse(httpWebResponse);

                if (httpWebResponse != null)
                {
                    httpWebResponse.Dispose();
                }

            }
            catch (WebException webException)
            {
                response = new PhoneHomeHttpClientResponse();
            }
            catch (Exception exception)
            {
                response = new PhoneHomeHttpClientResponse();
            }

            return response;
        }


        private HttpWebResponse Sync(HttpWebRequest httpWebRequest)
        {
            try
            {
                return (HttpWebResponse)httpWebRequest.GetResponse();
            }
            catch (WebException wex)
            {
                if (wex.Response == null || wex.Status != WebExceptionStatus.ProtocolError)
                {
                    throw;
                }

                return this.HandleWebException(wex, httpWebRequest);
            }
        }

        private HttpWebResponse HandleWebException(WebException exception, HttpWebRequest request)
        {
            HttpWebResponse httpWebResponse = null;
            WebExceptionStatus responseStatus = exception.Status;

            if (exception.Response != null)
            {
                httpWebResponse = (HttpWebResponse)exception.Response;
            }

            return httpWebResponse;
        }



        private HttpWebRequest GetHttpWebRequest(PhoneHomeHttpClientRequest request)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(request.Url);

            httpWebRequest.Method = request.Method;
            httpWebRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            httpWebRequest.Timeout = request.TimeoutInMS ?? 1000;

            httpWebRequest.AllowAutoRedirect = request.FollowRedirects;

            if (!string.IsNullOrWhiteSpace(request.Accept))
            {
                httpWebRequest.Accept = request.Accept;
            }

            foreach (var header in request.Headers)
            {
                httpWebRequest.Headers.Set(header.Key, header.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.ContentType) && request.Method.ToLower() != "get")
            {
                httpWebRequest.ContentType = request.ContentType;
            }

            if (!string.IsNullOrWhiteSpace(request.UserAgent))
            {
                httpWebRequest.UserAgent = request.UserAgent;
            }

            if (request.Body != null)
            {
                byte[] body = Encoding.UTF8.GetBytes(request.Body);

                httpWebRequest.ContentLength = body == null ? 0 : body.Length;

                if (body != null)
                {
                    using (var rs = httpWebRequest.GetRequestStream())
                    {
                        rs.Write(body, 0, body.Length);
                    }
                }
            }

            return httpWebRequest;
        }
    }
}
