using System;

namespace TradingTools.Models.Stocks
{
    public class StockPrice
    {
        /// <summary>
        /// Formatted as YYYY-MM-DD
        /// </summary>
        public DateTimeOffset? Date { get; set; }
        /// <summary>
        /// Adjusted data for historical dates. Split adjusted only.
        /// </summary>
        public decimal? Open { get; set; }
        /// <summary>
        /// Adjusted data for historical dates. Split adjusted only.
        /// </summary>
        public decimal? Close { get; set; }
        /// <summary>
        /// Adjusted data for historical dates. Split adjusted only.
        /// </summary>
        public decimal? High { get; set; }
        /// <summary>
        /// Adjusted data for historical dates. Split adjusted only.
        /// </summary>
        public decimal? Low { get; set; }
        /// <summary>
        /// Adjusted data for historical dates. Split adjusted only.
        /// </summary>
        public decimal? Volume { get; set; }


        /// <summary>
        /// Unadjusted data for historical dates
        /// </summary>
        public decimal? UOpen { get; set; }
        /// <summary>
        /// Unadjusted data for historical dates.
        /// </summary>
        public decimal? UClose { get; set; }
        /// <summary>
        /// Unadjusted data for historical dates.
        /// </summary>
        public decimal? UHigh { get; set; }
        /// <summary>
        /// Unadjusted data for historical dates.
        /// </summary>
        public decimal? ULow { get; set; }
        /// <summary>
        /// Unadjusted data for historical dates.
        /// </summary>
        public decimal? UVolume { get; set; }

        /// <summary>
        /// hange from previous trading day.
        /// </summary>
        public decimal? Change { get; set; }
        /// <summary>
        /// Change percent from previous trading day.
        /// </summary>
        public decimal? ChangePercent { get; set; }

        /// <summary>
        /// Percent change of each interval relative to first value. Useful for comparing multiple stocks.
        /// </summary>
        public decimal? ChangeOverTime { get; set; }


        public decimal LowerShadow => Close ?? 0 - Low ?? 0;
        public decimal UpperShadow => High ?? 0 - Open ?? 0;
        public bool HasShortBody => Math.Abs(ChangePercent ?? 0) < 1 && Math.Abs(ChangePercent ?? 0) > 0;
    }
}
