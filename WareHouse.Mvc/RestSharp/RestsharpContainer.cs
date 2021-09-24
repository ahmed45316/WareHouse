using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;

namespace WareHouse.Mvc.RestSharp
{
    public class RestsharpContainer : IRestsharpContainer
    {
        private readonly RestClient _client;
        private readonly string _serverUri;
        public RestsharpContainer(string serverUri)
        {
            _serverUri = serverUri;
            _client = new RestClient();
        }
        public async Task<T> SendRequest<T>(string uri, Method method, object obj = null)
        {
            _client.CookieContainer = new CookieContainer();
            var request = new RestRequest($"{_serverUri}{uri}", method);
            _client.Timeout = -1;
            if (method == Method.POST || method == Method.PUT)
            {
                request.AddJsonBody(obj);
            }
            var response = await _client.ExecuteAsync<T>(request);
            T data;
            try { data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.Content); }
            catch (Exception) { data = default(T); }
            return data == null ? response.Data : data;
        }
        public async Task SendRequest(string uri, Method method, object obj = null)
        {
            _client.CookieContainer = new CookieContainer();
            var request = new RestRequest($"{_serverUri}{uri}", method);
            _client.Timeout = -1;
            if (method == Method.POST || method == Method.PUT)
            {
                request.AddJsonBody(obj);
            }
            await _client.ExecuteAsync(request);
        }

    }
}