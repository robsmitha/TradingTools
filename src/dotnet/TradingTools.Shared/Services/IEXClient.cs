using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TradingTools.Shared.Services
{
    public class IEXClient
    {
        static string BaseUrl { get; set; }
        static string Version { get; set; }
        static string Token { get; set; }
        public IEXClient(string baseUrl, string version, string token) 
        {
            BaseUrl = baseUrl;
            Version = version;
            Token = token;
        }

        private string RequestUri(string endpoint, Dictionary<string, string> @params)
        {
            var requestUri = new StringBuilder();
            requestUri.Append(BaseUrl);
            requestUri.Append($"/{Version}");
            requestUri.Append($"/{endpoint}");
            requestUri.Append($"?token={Token}");

            if (@params != null)
                foreach (var k in @params.Keys)
                    requestUri.Append($"&{k}={@params[k]}");

            return requestUri.ToString();
        }

        public async Task<T> SendAsync<T>(string endpoint, Dictionary<string, string> @params = null)
        {
            using HttpClient client = new HttpClient();
            try
            {
                var uri = RequestUri(endpoint, @params);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return JsonSerializer.Deserialize<T>(result, options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }

                return default;
            }
            catch (HttpRequestException e)
            {
                throw e;
            }
        }

    }
}
