using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneHome.Client.Http
{
    public class AcceptType
    {
        public const string ApplicationJson = "application/json";
    }

    public class ContentType
    {
        public const string ApplicationJson = "application/json";
        public const string Form = "application/x-www-form-urlencoded";
        public const string Xml = "text/xml";
        public const string Jpg = "image/jpeg";
        public const string Stream = "application/octet-stream";
        public const string PlainText = "text/plain";
    }

    public class PhoneHomeHttpClientRequest
    {
        internal string Body { get; set; }

        public string Url { get; protected set; }

        public string Method { get; protected set; }

        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// The content type 
        /// </summary>
        public string ContentType { get; set; }

        public bool FollowRedirects { get; set; }

        public int? TimeoutInMS { get; set; }

        public string Accept { get; set; }

        public string UserAgent { get; set; }

        protected PhoneHomeHttpClientRequest()
        {
            TimeoutInMS = 10000;
            Headers = new Dictionary<string, string>();
        }

        public static PhoneHomeHttpClientRequest Get(string url, string acceptType = AcceptType.ApplicationJson)
        {
            PhoneHomeHttpClientRequest request = new PhoneHomeHttpClientRequest();
            request.Url = url;
            request.Method = "GET";
            request.Accept = acceptType;

            return request;
        }

        public static PhoneHomeHttpClientRequest Post(string url, string body, string contentType = Http.ContentType.ApplicationJson, string acceptType = Http.AcceptType.ApplicationJson)
        {
            PhoneHomeHttpClientRequest request = new PhoneHomeHttpClientRequest();
            request.Url = url;
            request.Method = "POST";
            request.Body = body;
            request.ContentType = contentType;
            request.Accept = acceptType;

            return request;
        }

        public static PhoneHomeHttpClientRequest Put(string url, string body, string contentType = Http.ContentType.ApplicationJson, string acceptType = Http.AcceptType.ApplicationJson)
        {
            PhoneHomeHttpClientRequest request = new PhoneHomeHttpClientRequest();
            request.Url = url;
            request.Method = "PUT";
            request.Body = body;
            request.ContentType = contentType;
            request.Accept = acceptType;

            return request;
        }
    }
}
