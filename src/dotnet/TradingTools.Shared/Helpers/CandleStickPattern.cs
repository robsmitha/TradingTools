using System.Collections.Generic;
using System.Linq;
using TradingTools.Models.Stocks;

namespace TradingTools.Shared.Helpers
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
        public CandleStickPattern(string symbol, CandleStickPatterns pattern, LinkedListNode<StockPrice> node)
        {
            Symbol = symbol;
            Pattern = pattern;
            TotalVolume = node?.BullishVolume() ?? 0;
            StockPrice = node?.Value ?? new StockPrice();
        }
        public string Symbol { get; set; }
        public decimal TotalVolume { get; set; }
        public StockPrice StockPrice { get; set; }
        public CandleStickPatterns Pattern { get; set; }
    }
}
