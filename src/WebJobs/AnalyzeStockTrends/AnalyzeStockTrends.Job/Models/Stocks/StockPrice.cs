using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzeStockTrends.Job.Models.Stocks
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

        /// <summary>
        /// the stock
        /// </summary>
        public string Symbol { get; set; }
    }
}
