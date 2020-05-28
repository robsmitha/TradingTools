using System.Collections.Generic;
using AnalyzeStockTrends.Job.Models.Stocks;

namespace AnalyzeStockTrends.Job.Utilities
{
    public enum CandleStickTypes
    {
        Bullish,
        Bearish,
        Unknown
    }
    public class CandleStick
    {
        public CandleStick(StockPrice _) : this(_?.Open, _?.Close, _?.Low, _?.High, _?.Volume, _?.ChangePercent) { }

        public CandleStick(decimal? open, decimal? close, decimal? low, decimal? high, decimal? volume, decimal? changePercent)
        {
            Open = open ?? 0;
            Close = close ?? 0;
            Low = low ?? 0;
            High = high ?? 0;
            Volume = volume ?? 0;
            ChangePercent = changePercent ?? 0;
        }

        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Volume { get; set; }
        public decimal ChangePercent { get; set; }

        public CandleStickTypes CandleStickType
        {
            get
            {
                //the closing price for the period was less than the opening price; 
                //hence, it is bearish and indicates selling pressure.
                if (Close < Open) return CandleStickTypes.Bearish;

                //the closing price was greater than the opening price. 
                //This is bullish and shows buying pressure.
                if (Close > Open) return CandleStickTypes.Bullish;

                return CandleStickTypes.Unknown;
            }
        }

        /// <summary>
        /// The Hammer is a bullish reversal pattern, which signals that a stock is nearing bottom in a downtrend. 
        /// The body of the candle is short with a longer lower shadow which is a sign of sellers driving prices lower during the trading session, 
        /// only to be followed by strong buying pressure to end the session on a higher close. 
        /// Before we jump in on the bullish reversal action, however, we must confirm the upward trend by watching it closely for the next few days. 
        /// The reversal must also be validated through the rise in the trading volume.
        /// </summary>
        //TODO: check longer low/hi is signifcantly longer than the couterpart
        //TODO: Determine some threshold to determine what is consided a longer lower shadow (maybe high minus low?) 
        public bool IsHammer() => Low - Close > 0; //longer lower shadow

        /// <summary>
        /// The Inverted Hammer also forms in a downtrend and represents a likely trend reversal or support. 
        /// It’s identical to the Hammer except for the longer upper shadow, which indicates buying pressure after the opening price, followed by considerable selling pressure, which however wasn’t enough to bring the price down below its opening value. 
        /// Again, bullish confirmation is required, and it can come in the form of a long hollow candlestick or a gap up, accompanied by a heavy trading volume.
        /// </summary>
        /// <returns></returns>
        //TODO: check longer low/hi is signifcantly longer than the couterpart
        //TODO: Determine some threshold to determine what is consided a longer upper shadow (maybe low minus high?) 
        public bool IsInvertedHammer() => High - Open > 0; //longer upper shadow


        /// <summary>
        /// As the name indicates, the Morning Star is a sign of hope and a new beginning in a gloomy downtrend. 
        /// The pattern consists of three candles: one short-bodied candle (called a doji or a spinning top) between a preceding long black candle and a succeeding long white one. 
        /// The color of the real body of the short candle can be either white or black, and there is no overlap between its body and that of the black candle before. 
        /// It shows that the selling pressure that was there the day before is now subsiding. 
        /// The third white candle overlaps with the body of the black candle and shows a renewed buyer pressure and a start of a bullish reversal, especially if confirmed by the higher volume.
        /// </summary>
        /// <param name="before"></param>
        /// <returns></returns>
        public bool IsMorningStar(CandleStick before) => before.Close > Close;

        /// <summary>
        /// Similar to the engulfing pattern, the Piercing Line is a two-candle bullish reversal pattern, also occurring in downtrends. 
        /// The first long black candle is followed by a white candle that opens lower than the previous close. 
        /// Soon thereafter, the buying pressure pushes the price up halfway or more (preferably two-thirds of the way) into the real body of the black candle.
        /// </summary>
        /// <param name="before"></param>
        /// <returns></returns>
        public bool IsPiercingLine(CandleStick before) => Open < before.Close;

        /// <summary>
        /// The Bullish Engulfing pattern is a two-candle reversal pattern. 
        /// The second candle completely ‘engulfs’ the real body of the first one, without regard to the length of the tail shadows. 
        /// The Bullish Engulfing pattern appears in a downtrend and is a combination of one dark candle followed by a larger hollow candle. 
        /// On the second day of the pattern, price opens lower than the previous low, yet buying pressure pushes the price up to a higher level than the previous high, culminating in an obvious win for the buyers. 
        /// It is advisable to enter a long position when the price moves higher than the high of the second engulfing candle—in other words when the downtrend reversal is confirmed.
        /// </summary>
        /// <param name="before"></param>
        /// <returns></returns>
        public bool IsBullishEngulfing(CandleStick before) => High > before.High && Low < before.Low;

    }
}
