using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace ghumpo.web.Core
{
    public class WebApiCoreRestSharp<T, T1> : IWebApiCore<T, T1>
    {
        //private const string PreUri = "http://localhost:2341/";
        private const string PreUri = "http://testghumpo.azurewebsites.net/";

        public WebApiCoreRestSharp(string uri)
        {
            Uri = uri;
        }

        private string Uri { get; }

        public async Task<Tuple<bool, string>> PostAsync(T obj)
        {
            var client = new RestClient(PreUri);
            //client.Authenticator = new SimpleAuthenticator("username", UserName, "password", Password);
            var request = new RestRequest(Uri, Method.POST) {RequestFormat = DataFormat.Json};
            request.AddBody(obj);
            var cancellationTokenSource = new CancellationTokenSource();
            var response = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);
            var responseMessage = string.Format("{0} successfully Added",
                obj.ToString().Split('.')[obj.ToString().Split('.').Length - 1]);
            if (response != null && (response.ResponseStatus == ResponseStatus.Completed))
            {
                if (!string.IsNullOrEmpty(response.Content))
                    responseMessage = Convert.ToString(JObject.Parse(response.Content)["Message"]);
                if (response.StatusCode == HttpStatusCode.OK)
                    return new Tuple<bool, string>(true, responseMessage);
                return new Tuple<bool, string>(false, responseMessage);
            }
            return null;
        }

        public async Task<T> GetAsync(T1 id)
        {
            var client = new RestClient();
            //     client.Authenticator = new SimpleAuthenticator("username", UserName, "password", Password);
            client.BaseUrl = new Uri(PreUri + Uri + "/" + id);
            var request = new RestRequest(Method.GET) {RequestFormat = DataFormat.Json};
            var cancellationTokenSource = new CancellationTokenSource();

            var response = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);

            if (response != null && ((response.StatusCode == HttpStatusCode.OK) &&
                                     (response.ResponseStatus == ResponseStatus.Completed)))
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            return default(T);
        }

        public async Task<bool> PutAsync(T obj)
        {
            var client = new RestClient(PreUri);
            //   client.Authenticator = new SimpleAuthenticator("username", UserName, "password", Password);
            var request = new RestRequest(Uri, Method.PUT) {RequestFormat = DataFormat.Json};
            request.AddBody(obj);
            var cancellationTokenSource = new CancellationTokenSource();
            var response = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);

            if (response != null && ((response.StatusCode == HttpStatusCode.OK) &&
                                     (response.ResponseStatus == ResponseStatus.Completed)))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(T1 id)
        {
            var client = new RestClient(PreUri);
            //    client.Authenticator = new SimpleAuthenticator("username", UserName, "password", Password);
            var request = new RestRequest(Uri + "/" + id, Method.DELETE) {RequestFormat = DataFormat.Json};
            var cancellationTokenSource = new CancellationTokenSource();
            var response = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);

            if (response != null && ((response.StatusCode == HttpStatusCode.OK) &&
                                     (response.ResponseStatus == ResponseStatus.Completed)))
            {
                return true;
            }
            return false;
        }
    }
}