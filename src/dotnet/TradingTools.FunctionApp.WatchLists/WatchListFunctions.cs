using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TradingTools.Models.Stocks;
using TradingTools.Shared.Helpers;
using TradingTools.Shared.Services;

namespace TradingTools.FunctionApp.WatchLists
{
    public static class WatchListFunctions
    {
        public static readonly IEXClient iex = new IEXClient(
            baseUrl: Environment.GetEnvironmentVariable("IEX_BaseUrl"),
            version: Environment.GetEnvironmentVariable("IEX_Version"),
            token: Environment.GetEnvironmentVariable("IEX_Token"));

        [FunctionName("AnalyzeTradingPatterns")]
        public static void AnalyzeTradingPatterns([TimerTrigger("*/30 * * * * *")]TimerInfo myTimer,
             [CosmosDB(CosmosDbConstants.WatchLists, CosmosDbConstants.Symbols, ConnectionStringSetting = CosmosDbConstants.ConnectionStringName,
                SqlQuery ="SELECT * FROM c WHERE c.user='%default_user%' ORDER BY c._ts DESC")] IEnumerable<dynamic> watchListSymbols,
              [CosmosDB(CosmosDbConstants.TradingPatterns, CosmosDbConstants.Patterns, ConnectionStringSetting = CosmosDbConstants.ConnectionStringName)] IAsyncCollector<CandleStickPattern> patternsOut,
             ILogger log)
        {
            log.LogInformation($"Anaylyze downtrend reversals function executed at: {DateTime.Now}");

            var symbols = watchListSymbols?.Select(wl => (string)wl.symbol) ?? Environment.GetEnvironmentVariable("WatchListSymbols").Split(',', StringSplitOptions.RemoveEmptyEntries);
            string range = "5d";
            var patterns = new List<CandleStickPattern>();
            foreach (var symbol in symbols)
            {
                var prices = iex.SendAsync<List<StockPrice>>(endpoint: $"stock/{symbol}/chart/{range}", @params: new Dictionary<string, string> { { "includeToday", "false" } }).Result;
                var candles = new LinkedList<StockPrice>(prices);
                for (var current = candles.First; current != null; current = current.Next)
                {
                    if (current.IsHammer())
                        patterns.Add(new CandleStickPattern(symbol, CandleStickPatterns.Hammer, current));

                    if (current.IsInvertedHammer())
                        patterns.Add(new CandleStickPattern(symbol, CandleStickPatterns.InvertedHammer, current));

                    if (current.IsMorningStar())
                        patterns.Add(new CandleStickPattern(symbol, CandleStickPatterns.MorningStar, current));

                    if (current.IsPiercingLine())
                        patterns.Add(new CandleStickPattern(symbol, CandleStickPatterns.PiercingLine, current));

                    if (current.IsBullishEngulfing())
                        patterns.Add(new CandleStickPattern(symbol, CandleStickPatterns.BullishEngulfing, current));
                }
            }

            foreach (var pattern in patterns)
            {
                //save possible observed candle stick patterns
                patternsOut.AddAsync(pattern);

                log.LogInformation("{@symbol} had possible {@pattern} on {@date}, followed by a volume of {@TotalVolume}.",
                    pattern.symbol,
                    pattern.Pattern,
                    pattern.StockPrice.Date.Value.UtcDateTime.ToShortDateString(),
                    pattern.TotalVolume);
            }
        }

        [FunctionName("SymbolsTrigger")]
        public static void SymbolsTrigger([CosmosDBTrigger(CosmosDbConstants.WatchLists, CosmosDbConstants.Symbols, ConnectionStringSetting = CosmosDbConstants.ConnectionStringName, 
            LeaseCollectionName = "leases")]IReadOnlyList<Document> input,
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);
            }
        }

        [FunctionName("GetWatchList")]
        public static async Task<IActionResult> GetWatchList(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetWatchList/{user}")] HttpRequest req,
            [CosmosDB(CosmosDbConstants.WatchLists, CosmosDbConstants.Symbols, ConnectionStringSetting = CosmosDbConstants.ConnectionStringName,
                SqlQuery ="SELECT * FROM c WHERE c.user={user} ORDER BY c._ts DESC")] IEnumerable<dynamic> symbols,
            ILogger log,
            string user)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            if(symbols == null)
            {
                return new OkObjectResult($"No watch list found for {user}");
            }


            return new OkObjectResult(symbols);
        }

        [FunctionName("AddSymbolToWatchList")]
        public static IActionResult AddSymbolToWatchList(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, 
            [CosmosDB(CosmosDbConstants.WatchLists, CosmosDbConstants.Symbols, ConnectionStringSetting = CosmosDbConstants.ConnectionStringName)] out string docs,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            docs = requestBody;

            dynamic data = JsonConvert.DeserializeObject(requestBody);
            return new OkObjectResult(data);
        }
    }
}
