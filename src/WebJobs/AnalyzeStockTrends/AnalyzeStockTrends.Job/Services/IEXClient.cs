using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AnalyzeStockTrends.Job.Services
{
    public class IEXClient : IIEXClient
    {
        static string BaseUrl { get; set; }
        static string Version { get; set; }
        static string Token { get; set; }
        public IEXClient(IConfiguration configuration) 
        {
            BaseUrl = configuration["Configurations:IEX_BaseUrl"];
            Version = configuration["Configurations:IEX_Version"];
            Token = configuration["Configurations:IEX_Token"];
        }

        private string RequestUri(string endpointPath, string @params)
        {
            var requestUri = new StringBuilder();
            requestUri.Append(BaseUrl);
            requestUri.Append($"/{Version}");
            requestUri.Append($"/{endpointPath}");
            requestUri.Append($"?token={Token}");
            if(@params != null)
            {
                foreach (var kv in @params.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    requestUri.Append($"&{kv}");
            }
            return requestUri.ToString();
        }

        public async Task<T> SendAsync<T>(string function, string @params = null)
        {
            using HttpClient client = new HttpClient();
            try
            {
                var uri = RequestUri(function, @params);
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
