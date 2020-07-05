using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TradingTools.Models.Stocks;
using TradingTools.Shared.Helpers;
using TradingTools.Shared.Services;

namespace TradingTools.FunctionApp.TradingPatterns
{
    public static class TradingPatternFunctions
    {
        static string DefaultRange = Environment.GetEnvironmentVariable("DefaultRange") ?? "5d";
        public static readonly IEXClient iex = new IEXClient(
            baseUrl: Environment.GetEnvironmentVariable("IEX_BaseUrl"),
            version: Environment.GetEnvironmentVariable("IEX_Version"),
            token: Environment.GetEnvironmentVariable("IEX_Token"));

        [FunctionName("AnalyzeTradingPatterns")]
        public static void AnalyzeTradingPatterns([TimerTrigger("0 0 */2 * * *")]TimerInfo myTimer,
             [CosmosDB(CosmosDbConstants.DatabaseName, CosmosDbConstants.WatchListSymbols, ConnectionStringSetting = CosmosDbConstants.ConnectionStringName,
                SqlQuery ="SELECT * FROM c WHERE c.UserID='%default_user_id%' ORDER BY c._ts DESC")] IEnumerable<dynamic> watchListSymbols,
              [CosmosDB(CosmosDbConstants.DatabaseName, CosmosDbConstants.TradingPatterns, ConnectionStringSetting = CosmosDbConstants.ConnectionStringName)] IAsyncCollector<CandleStickPattern> patternsOut,
             ILogger log)
        {
            log.LogInformation($"Anaylyze downtrend reversals function executed at: {DateTime.Now}");

            var symbols = watchListSymbols?.Select(wl => (string)wl.Symbol) 
                ?? Environment.GetEnvironmentVariable("WatchListSymbols").Split(',', StringSplitOptions.RemoveEmptyEntries);

            var patterns = new List<CandleStickPattern>();
            foreach (var symbol in symbols)
            {
                var prices = iex.SendAsync<List<StockPrice>>(
                    endpoint: $"stock/{symbol}/chart/{DefaultRange}", 
                    @params: new Dictionary<string, string> { { "includeToday", "false" } }).Result;
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

                log.LogInformation("{@symbol} had possible {@pattern} on {@date}, followed by a volume of {@SubsequentVolume}.",
                    pattern.Symbol,
                    pattern.Pattern,
                    pattern.Date.ToShortDateString(),
                    pattern.SubsequentVolume);
            }
        }

        [FunctionName("GetTradingPatterns")]
        public static async Task<IActionResult> GetTradingPatterns(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetTradingPatterns/{date}")] HttpRequest req,
            [CosmosDB(CosmosDbConstants.DatabaseName, CosmosDbConstants.TradingPatterns,
            ConnectionStringSetting = CosmosDbConstants.ConnectionStringName,
            SqlQuery ="SELECT * FROM c ORDER BY c._ts DESC")] IEnumerable<dynamic> patterns,
            ILogger log,
            string date)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            var result = patterns.Select(p => new {
                p.id,
                p.Symbol,
                p.Pattern,
                p.SubsequentVolume,
                Date = ((DateTime)p.Date).ToShortDateString(),
                p.Open,
                p.Close,
                p.High,
                p.Low,
                p.Volume,
                p.Change,
                p.ChangePercent,
                p.ChangeOverTime,
                p.LowerShadow,
                p.UpperShadow
            });

            return await Task.FromResult(new OkObjectResult(result));
        }

        [FunctionName("AddTradingPattern")]
        public static IActionResult AddTradingPattern(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            [CosmosDB(CosmosDbConstants.DatabaseName, CosmosDbConstants.TradingPatterns, 
            ConnectionStringSetting = CosmosDbConstants.ConnectionStringName)] out string docs,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            docs = requestBody;

            dynamic data = JsonConvert.DeserializeObject(requestBody);
            return new OkObjectResult(data);
        }

        [FunctionName("TradingPatternsTrigger")]
        public static void TradingPatternsTrigger([CosmosDBTrigger(CosmosDbConstants.DatabaseName, CosmosDbConstants.TradingPatterns, 
            ConnectionStringSetting = CosmosDbConstants.ConnectionStringName,
            LeaseCollectionName = CosmosDbConstants.LeaseCollectionName)] IReadOnlyList<Document> input, 
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);
            }
        }
    }
}
