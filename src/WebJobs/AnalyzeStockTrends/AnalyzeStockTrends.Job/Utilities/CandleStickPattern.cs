using System.Collections.Generic;
using System.Linq;
using AnalyzeStockTrends.Job.Models.Stocks;

namespace AnalyzeStockTrends.Job.Utilities
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
        public CandleStickPattern(string symbol, CandleStickPatterns pattern, int i, List<StockPrice> range)
        {
            Symbol = symbol;
            Pattern = pattern;
            Volume = BullishVolume(i, range);
        }
        public string Symbol { get; set; }
        public decimal Volume { get; set; }
        public CandleStickPatterns Pattern { get; set; }

        /// <summary>
        /// Most bullish reversal patterns require bullish confirmation. 
        /// In other words, they must be followed by an upside price move which can come as a long hollow candlestick or a gap up and be accompanied by high trading volume. 
        /// This confirmation should be observed within three days of the pattern.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public decimal BullishVolume(int i, List<StockPrice> range)
        {
            //Before we jump in on the bullish reversal action,
            //we must confirm the upward trend by watching it closely for the next few days.
            var volume = 0M;
            DFS(i + 1, range, ref volume);
            return volume;
        }

        static void DFS(int i, List<StockPrice> range, ref decimal volume)
        {
            //confirm the upward trend by watching it closely for the next few days. 
            if (i < range.Count)
            {
                var candleStick = new CandleStick(range[i]);

                if (candleStick.CandleStickType == CandleStickTypes.Bullish)  //the stock rose
                {
                    //The reversal must also be validated through the rise in the trading volume.
                    volume += candleStick.Volume;
                    DFS(i + 1, range, ref volume);
                }
            }
        }
    }
}
