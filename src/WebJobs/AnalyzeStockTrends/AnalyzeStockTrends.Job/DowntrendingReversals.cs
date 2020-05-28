using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnalyzeStockTrends.Job.Models.Stocks;
using AnalyzeStockTrends.Job.Services;
using AnalyzeStockTrends.Job.Utilities;

namespace AnalyzeStockTrends.Job
{
    public class DowntrendingReversals
    {
        private IIEXClient _edx { get; set; }
        public DowntrendingReversals(IIEXClient edx)
        {
            _edx = edx;
        }
        /// <summary>
        /// Program analyzes trading prices/volume/etc. to observe and report candle stick reversal patterns in a given date/time range (i.e. 1mm, 5d, 1m, 3m, 6m. 1y)
        /// </summary>
        /// <param name="downtrending">downtrending stock to analyze</param>
        /// <returns></returns>
        public async Task Run(string[] downtrending, string range = "5d")
        {
            var reversalPatterns = new List<CandleStickPattern>();
            foreach (var symbol in downtrending)
            {
                var datetimeRange = await _edx.SendAsync<List<StockPrice>>($"stock/{symbol}/chart/{range}");

                var trend = new Stack<CandleStick>();

                for (int i = 0; i < datetimeRange.Count; i++)
                {
                    var candle = new CandleStick(datetimeRange[i]);

                    if (candle.CandleStickType == CandleStickTypes.Bearish)
                    {
                        trend.Push(candle); //add to down trend
                    }
                    else if (candle.CandleStickType == CandleStickTypes.Bullish && trend.Any())
                    {
                        //check for candle stick reversal patterns
                        var before = trend.Pop();

                        if (Math.Abs(candle.ChangePercent) < Math.Abs(before.ChangePercent))
                        {
                            if (Math.Abs(candle.ChangePercent) < 1)  //TODO: Determine some threshold to determine what is consided a short body (change percent less than?)
                            {
                                if (candle.IsHammer())
                                {
                                    reversalPatterns.Add(new CandleStickPattern(symbol, CandleStickPatterns.Hammer, i, datetimeRange));
                                    break;
                                }
                                else if (candle.IsInvertedHammer())
                                {
                                    reversalPatterns.Add(new CandleStickPattern(symbol, CandleStickPatterns.InvertedHammer, i, datetimeRange));
                                    break;
                                }
                                else if (candle.IsMorningStar(before))
                                {
                                    reversalPatterns.Add(new CandleStickPattern(symbol, CandleStickPatterns.MorningStar, i, datetimeRange));
                                    break;
                                }
                            }
                            else
                            {
                                if (candle.IsPiercingLine(before))
                                {
                                    reversalPatterns.Add(new CandleStickPattern(symbol, CandleStickPatterns.PiercingLine, i, datetimeRange));
                                    break;
                                }
                            }
                        }
                        else if (Math.Abs(candle.ChangePercent) > Math.Abs(before.ChangePercent))
                        {
                            if (candle.IsBullishEngulfing(before))
                            {
                                reversalPatterns.Add(new CandleStickPattern(symbol, CandleStickPatterns.BullishEngulfing, i, datetimeRange));
                                break;
                            }
                        }
                    }
                }
            }

            Log.Information("Downtrending reversal patterns");
            foreach (var pattern in reversalPatterns)
            {
                Log.Information("{@symbol}\t{@pattern}={@volume}", pattern.Symbol, pattern.Pattern, pattern.Volume);
            }
        }
    }
}
