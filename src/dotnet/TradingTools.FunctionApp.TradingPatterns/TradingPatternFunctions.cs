using System;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using TradingTools.Models.Stocks;
using TradingTools.Shared.Helpers;
using TradingTools.Shared.Services;

namespace TradingTools.FunctionApp.TradingPatterns
{
    public static class TradingPatternFunctions
    {

        public static IEXClient iex = new IEXClient(
            baseUrl: Environment.GetEnvironmentVariable("IEX_BaseUrl"),
            version: Environment.GetEnvironmentVariable("IEX_Version"),
            token: Environment.GetEnvironmentVariable("IEX_Token"));

        [FunctionName("AnalyzeTradingPatterns")]
        public static void AnalyzeTradingPatterns([TimerTrigger("*/30 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Anaylyze downtrend reversals function executed at: {DateTime.Now}");
            var symbols = "ZM,NFLX,NVDA,CRM,TWLO,DOCU,SPY,MSFT,AAPL,AMZN,CA,QSR,MRNA,DKNG,TWTR,FCN,DIS,NKE,TEAM,CLX";
            string[] downtrending = symbols.Split(',', StringSplitOptions.RemoveEmptyEntries);
            string range = "5d";
            var patterns = new List<CandleStickPattern>();
            foreach (var symbol in downtrending)
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
                //possible observed candle stick patterns
                log.LogInformation("{@symbol} had possible {@pattern} on {@date}, followed by a volume of {@TotalVolume}.",
                    pattern.Symbol,
                    pattern.Pattern,
                    pattern.StockPrice.Date.Value.UtcDateTime.ToShortDateString(),
                    pattern.TotalVolume);
            }

            //TODO: Store predictions in cosmos db
        }
    }
}
