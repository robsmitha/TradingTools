using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TradingTools.Shared.Services
{
    public class LUISClient
    {
        private readonly string PredictionKey;

        private readonly string PredictionEndpoint;

        private readonly string AppId;

        private readonly string SlotName;

        private readonly string Version;
        public LUISClient(string predictionEndpoint, string predictionKey, string appId, string slotName, string version)
        {
            PredictionEndpoint = predictionEndpoint;
            PredictionKey = predictionKey;
            AppId = appId;
            SlotName = slotName;
            Version = version;
        }
        string GetRequestUri(string query)
        {
            var sb = new StringBuilder($"{PredictionEndpoint}/luis/prediction/{Version}/apps/{AppId}/slots/{SlotName}/predict?subscription-key={PredictionKey}");
            //sb.Append("&verbose=true");
            sb.Append("&show-all-intents=true");
            sb.Append("&log=true");
            sb.Append($"&query={query}");
            return sb.ToString();
        }
        public async Task<T> SendAsync<T>(string query)
        {
            using HttpClient client = new HttpClient();
            try
            {
                var uri = GetRequestUri(query);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(result);
                }
                return default;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
