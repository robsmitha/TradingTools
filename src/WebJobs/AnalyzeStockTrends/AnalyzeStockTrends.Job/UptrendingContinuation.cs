using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AnalyzeStockTrends.Job.Models.Stocks;
using AnalyzeStockTrends.Job.Services;
using AnalyzeStockTrends.Job.Utilities;

namespace AnalyzeStockTrends.Job
{
    public class UptrendingContinuation
    {

        private IIEXClient _edx { get; set; }
        public UptrendingContinuation(IIEXClient edx)
        {
            _edx = edx;
        }
        public async Task Run(string[] uptrending, string range = "5d")
        {
            var continuationPatterns = new List<CandleStickPattern>();
            foreach (var symbol in uptrending)
            {
                var datetimeRange = await _edx.SendAsync<List<StockPrice>>($"stock/{symbol}/chart/{range}");

                var trend = new Stack<CandleStick>();

                for (int i = 0; i < datetimeRange.Count; i++)
                {
                    var candle = new CandleStick(datetimeRange[i]);
                }
            }
            Log.Information("Uptrending continuation patterns");
            foreach (var pattern in continuationPatterns)
            {
                Log.Information("{@symbol}\t{@pattern}={@volume}", pattern.Symbol, pattern.Pattern, pattern.Volume);
            }
        }
    }
}
