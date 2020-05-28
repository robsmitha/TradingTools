namespace AnalyzeStockTrends.Job.Models.Stocks
{
    public class Quote
    {
        public string Symbol { get; set; }
        public string CompanyName { get; set; }
        public string PrimaryExchange { get; set; }
        public string CalculationPrice { get; set; }
        public object Open { get; set; }
        public object OpenTime { get; set; }
        public string OpenSource { get; set; }
        public object Close { get; set; }
        public object CloseTime { get; set; }
        public string CloseSource { get; set; }
        public object High { get; set; }
        public decimal? HighTime { get; set; }
        public string HighSource { get; set; }
        public object Low { get; set; }
        public decimal? LowTime { get; set; }
        public string LowSource { get; set; }
        public decimal? LatestPrice { get; set; }
        public string LatestSource { get; set; }
        public string LatestTime { get; set; }
        public decimal? LatestUpdate { get; set; }
        public object LatestVolume { get; set; }
        public decimal? IexRealtimePrice { get; set; }
        public decimal? IexRealtimeSize { get; set; }
        public decimal? IexLastUpdated { get; set; }
        public object DelayedPrice { get; set; }
        public object DelayedPriceTime { get; set; }
        public object OddLotDelayedPrice { get; set; }
        public object OddLotDelayedPriceTime { get; set; }
        public object ExtendedPrice { get; set; }
        public object ExtendedChange { get; set; }
        public object ExtendedChangePercent { get; set; }
        public object ExtendedPriceTime { get; set; }
        public decimal? PreviousClose { get; set; }
        public decimal? PreviousVolume { get; set; }
        public decimal? Change { get; set; }
        public decimal? ChangePercent { get; set; }
        public object Volume { get; set; }
        public decimal? IexMarketPercent { get; set; }
        public decimal? IexVolume { get; set; }
        public decimal? AvgTotalVolume { get; set; }
        public decimal? IexBidPrice { get; set; }
        public decimal? IexBidSize { get; set; }
        public decimal? IexAskPrice { get; set; }
        public decimal? IexAskSize { get; set; }
        public object IexOpen { get; set; }
        public object IexOpenTime { get; set; }
        public decimal? IexClose { get; set; }
        public decimal? IexCloseTime { get; set; }
        public decimal? MarketCap { get; set; }
        public decimal? PeRatio { get; set; }
        public decimal? Week52High { get; set; }
        public decimal? Week52Low { get; set; }
        public decimal? YtdChange { get; set; }
        public decimal? LastTradeTime { get; set; }
        public bool? IsUsMarketOpen { get; set; }
    }
}