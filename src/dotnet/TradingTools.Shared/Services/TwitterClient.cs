﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TradingTools.Shared.Services
{
    public class TwitterClient
    {
        static string BaseUrl { get; set; }
        static string Version { get; set; }
        static string Token { get; set; }
        public TwitterClient(string baseUrl, string version, string token)
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
            requestUri.Append("?");
            @params.Keys.ToList().ForEach(k => requestUri.Append($"{k}={@params[k]}&"));
            return requestUri.ToString();
        }

        public async Task<T> SendAsync<T>(string endpoint, Dictionary<string, string> @params = null)
        {
            using HttpClient client = new HttpClient();
            try
            {
                var uri = RequestUri(endpoint, @params);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(result);
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
