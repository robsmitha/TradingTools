using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnalyzeStockTrends.Job.Models.Stocks;
using AnalyzeStockTrends.Job.Services;
using AnalyzeStockTrends.Job.Aggregates.CandleStickPatternAggregate;
using AnalyzeStockTrends.Job.Extensions;

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
            var patterns = new List<CandleStickPattern>();
            foreach (var symbol in downtrending)
            {
                var prices = await _edx.SendAsync<List<StockPrice>>($"stock/{symbol}/chart/{range}", "includeToday=false");
                var candles = new LinkedList<StockPrice>();
                LinkedListNode<StockPrice> head = new LinkedListNode<StockPrice>(prices[0]);
                candles.AddFirst(head);
                prices.GetRange(1, prices.Count - 1).ForEach(stock => candles.AddLast(new LinkedListNode<StockPrice>(stock)));
                var down = new Stack<StockPrice>();
                var current = head.Next;
                while (current != null)
                {
                    var candleType = current.GetCandleStickType();
                    if (candleType == CandleStickTypes.RedFilled)
                    {
                        down.Push(current.Value);
                    }
                    else if (candleType == CandleStickTypes.GreenFilled || candleType == CandleStickTypes.GreenHollow || candleType == CandleStickTypes.RedHollow)
                    {
                        if (down.Any())
                        {
                            if (current.IsHammer())
                            {
                                patterns.Add(new CandleStickPattern(symbol, CandleStickPatterns.Hammer, current.BullishVolume()));
                                break;
                            }

                            if (current.IsInvertedHammer())
                            {
                                patterns.Add(new CandleStickPattern(symbol, CandleStickPatterns.InvertedHammer, current.BullishVolume()));
                                break;
                            }

                            if (current.IsMorningStar())
                            {
                                patterns.Add(new CandleStickPattern(symbol, CandleStickPatterns.MorningStar, current.BullishVolume()));
                                break;
                            }

                            if (current.IsPiercingLine())
                            {
                                patterns.Add(new CandleStickPattern(symbol, CandleStickPatterns.PiercingLine, current.BullishVolume()));
                                break;
                            }

                            if (current.IsBullishEngulfing())
                            {
                                patterns.Add(new CandleStickPattern(symbol, CandleStickPatterns.BullishEngulfing, current.BullishVolume()));
                                break;
                            }
                        }
                    }
                    current = current.Next;
                }
            }

            Log.Information("Downtrending reversal patterns");
            foreach (var pattern in patterns)
            {
                Log.Information("{@symbol}\t{@pattern}={@volume}", pattern.Symbol, pattern.Pattern, pattern.Volume);
            }
        }
    }
}
