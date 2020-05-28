using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;
using AnalyzeStockTrends.Job.Models.Stocks;
using AnalyzeStockTrends.Job.Services;
using AnalyzeStockTrends.Job.Aggregates.CandleStickPatternAggregate;

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
            var patterns = new List<CandleStickPattern>();
            foreach (var symbol in uptrending)
            {
                var prices = await _edx.SendAsync<List<StockPrice>>($"stock/{symbol}/chart/{range}");


            }
            Log.Information("Uptrending continuation patterns");
            foreach (var pattern in patterns)
            {
                Log.Information("{@symbol}\t{@pattern}={@volume}", pattern.Symbol, pattern.Pattern, pattern.Volume);
            }
        }
    }
}
