using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TradingTools.Models.Stocks;

namespace TradingTools.Shared.Helpers
{
    public enum CandleStickPatterns
    {
        [Description("Hammer")]
        Hammer,
        [Description("Inverted Hammer")]
        InvertedHammer,
        [Description("Bullish Engulfing")]
        BullishEngulfing,
        [Description("Piercing Line")]
        PiercingLine,
        [Description("Morning Star")]
        MorningStar
    }
    public class CandleStickPattern
    {
        public CandleStickPattern(string symbol, CandleStickPatterns pattern, LinkedListNode<StockPrice> node)
        {
            Symbol = symbol;
            Pattern = pattern.GetEnumDescription();
            SubsequentVolume = node.SubsequentVolume();

            Date = node.Value.Date.Value.UtcDateTime;
            Open = node.Value.Open ?? 0;
            Close = node.Value.Close ?? 0;
            High = node.Value.High ?? 0;
            Low = node.Value.Low ?? 0;
            Volume = node.Value.Volume ?? 0;

            Change = node.Value.Change ?? 0;
            ChangePercent = node.Value.ChangePercent ?? 0;
            ChangeOverTime = node.Value.ChangeOverTime ?? 0;

            LowerShadow = node.Value.LowerShadow;
            UpperShadow = node.Value.UpperShadow;
        }
        public string Symbol { get; set; }
        public decimal SubsequentVolume { get; set; }
        public string Pattern { get; set; }
        public DateTime Date { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Volume { get; set; }
        public decimal Change { get; set; }
        public decimal ChangePercent { get; set; }
        public decimal ChangeOverTime { get; set; }
        public decimal LowerShadow { get; set; }
        public decimal UpperShadow { get; set; }
    }
}
