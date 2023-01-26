using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Script.Serialization;

namespace eSetter.Models
{
    public class HTTPHandler
    {
        // Some default settings
        const string UserAgent = "Bot"; // Change this to something more meaningful
        const int TimeOut = 1000; // Time out in ms

        public static string getHostAddress()
        {
            return ConfigurationManager.AppSettings["HostAddress"];
        }

        public static dynamic CreateRequest(List<string> body)
        {
            dynamic contentApp;

            string url = String.Format("https://{0}:8443/api/v2/cli/mail/call", getHostAddress());

            string token = GetToken();
            HttpWebRequest req = HttpWebRequest.Create(url) as HttpWebRequest;
            req.Headers.Add("X-API-Key", token);
            req.Method = "POST";

            req.UserAgent = "eSetter";

            var data = new Dictionary<string, List<string>>
                {
                    {"params", body}
                };

            var jsonData = JsonConvert.SerializeObject(data);


            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                string json = jsonData;

                streamWriter.Write(json);
                streamWriter.Flush();
            }
            WebResponse resp = req.GetResponse();
            Stream receiveStream = resp.GetResponseStream();

            using (StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8))
            {
                contentApp = Json.Decode(reader.ReadToEnd());
            }

            return contentApp;
        }

        // Basic connection
        public static string Connect(string url)
        {
            return Connect(url, "", "", UserAgent, "", TimeOut);
        }

        // Connect with post data passed as a key : value pair dictionary
        public static string Connect(string url, Dictionary<string, string> args)
        {
            return Connect(url, "", "", UserAgent, ToQueryString(args), TimeOut);
        }

        // Connect with a custom user agent specified
        public static string Connect(string url, string userAgent)
        {
            return Connect(url, "", "", userAgent, "", TimeOut);
        }

        public static string Connect(string url, string userName, string password, string userAgent, string postData, int timeOut)
        {
            string result;

            try
            {
                // Create a request for the URL.        
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                if (userAgent == null)
                    userAgent = UserAgent;

                request.UserAgent = userAgent;
                request.Timeout = timeOut;

                if (userName.Length > 0)
                {
                    string authInfo = userName + ":" + password;
                    authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                    request.Headers["Authorization"] = "Basic " + authInfo;
                    request.AllowAutoRedirect = false;
                }

                if (postData.Length > 0)
                {
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.AllowAutoRedirect = false;

                    // Create POST data and convert it to a byte array.
                    byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                    request.ContentLength = byteArray.Length;
                    using (Stream dataStream = request.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                    }
                }

                // Get the response.
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // Get the stream containing content returned by the server.
                    Stream dataStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        result = string.Format("Server response:\n{0}\n{1}", response.StatusDescription, reader.ReadToEnd());
                    }
                }
            }

            catch (Exception e)
            {
                result = string.Format("There was an error:\n{0}", e.Message);
            }

            return result;
        }

        public static string GetToken()
        {
            string token = String.Empty;
            dynamic contentApp;
            string url = String.Format("https://{0}:8443/api/v2/auth/keys", getHostAddress());

            HttpWebRequest req = HttpWebRequest.Create(url) as HttpWebRequest;
            string authInfo = ConfigurationManager.AppSettings["UserName"] + ":" + ConfigurationManager.AppSettings["Password"];
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            req.Headers["Authorization"] = "Basic " + authInfo;
            req.Method = "POST";

            req.UserAgent = "eSetter";
            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                string json = "{}";

                streamWriter.Write(json);
                streamWriter.Flush();
            }
            WebResponse resp = req.GetResponse();
            Stream receiveStream = resp.GetResponseStream();

            using (StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8))
            {
                 contentApp = Json.Decode(reader.ReadToEnd());
                 token = contentApp.key;
            }
            return token;
        }

        public static string ToQueryString(Dictionary<string, string> args)
        {
            List<string> encodedData = new List<string>();

            foreach (KeyValuePair<string, string> pair in args)
            {
                encodedData.Add(HttpUtility.UrlEncode(pair.Key) + "=" + HttpUtility.UrlEncode(pair.Value));
            }

            return String.Join("&", encodedData.ToArray());
        }
    }
}