using System.Collections.Generic;
using System.Linq;

namespace AnalyzeStockTrends.Job.Aggregates.CandleStickPatternAggregate
{
    public enum CandleStickPatterns
    {
        Hammer,
        InvertedHammer,
        BullishEngulfing,
        PiercingLine,
        MorningStar
    }
    public class CandleStickPattern
    {
        public CandleStickPattern(string symbol, CandleStickPatterns pattern, decimal volume)
        {
            Symbol = symbol;
            Pattern = pattern;
            Volume = volume;
        }
        public string Symbol { get; set; }
        public decimal Volume { get; set; }
        public CandleStickPatterns Pattern { get; set; }
    }
}
