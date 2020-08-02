using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TradingTools.Models.LUIS
{
    public class Response
    {
        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public string Query { get; set; }

        [JsonProperty("prediction", NullValueHandling = NullValueHandling.Ignore)]
        public Prediction Prediction{ get; set; }
    }

    public partial class Prediction
    {
        [JsonProperty("topIntent", NullValueHandling = NullValueHandling.Ignore)]
        public string TopIntent { get; set; }

        [JsonProperty("intents", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, Intent> Intents { get; set; }

        [JsonProperty("entities", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, List<Dictionary<string, List<string>>>> Entities { get; set; }
    }
    public partial class Intent
    {
        [JsonProperty("score", NullValueHandling = NullValueHandling.Ignore)]
        public decimal score { get; set; }
    }
    
}
