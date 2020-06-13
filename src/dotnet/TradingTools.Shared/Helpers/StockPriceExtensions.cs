using System;
using System.Collections.Generic;
using TradingTools.Models.Stocks;

namespace TradingTools.Shared.Helpers
{
    public static class StockPriceExtensions
    {
        /// <summary>
        /// The Hammer is a bullish reversal pattern, which signals that a stock is nearing bottom in a downtrend. 
        /// 
        /// Conditions:
        ///     1. The body of the candle is short
        ///     2. With a longer lower shadow
        ///     * This is a sign of sellers driving prices lower during the trading session, only to be followed by strong buying pressure to end the session on a higher close. 
        ///     
        /// Before we jump in on the bullish reversal action, however, we must confirm the upward trend by watching it closely for the next few days. 
        ///     * The reversal must also be validated through the rise in the trading volume.
        /// </summary>
        public static bool IsHammer(this LinkedListNode<StockPrice> node)
        {
            if (node == null || node.Previous == null) return false;
            var candleType = node.GetCandleStickType();
            if (candleType == CandleStickTypes.GreenHollow)
            {
                if (Math.Abs(node.Value.ChangePercent ?? 0) < Math.Abs(node.Previous.Value.ChangePercent ?? 0))
                {
                    //The body of the candle is short with a longer lower shadow
                    return node.Value.HasShortBody && node.Value.LowerShadow > node.Value.UpperShadow;
                }
            }

            return false;
        }

        /// <summary>
        /// The Inverted Hammer also forms in a downtrend and represents a likely trend reversal or support. 
        /// It’s identical to the Hammer except for the longer upper shadow, which indicates buying pressure after the opening price, followed by considerable selling pressure, which however wasn’t enough to bring the price down below its opening value. 
        /// Again, bullish confirmation is required, and it can come in the form of a long hollow candlestick or a gap up, accompanied by a heavy trading volume.
        /// </summary>
        /// <returns></returns>
        public static bool IsInvertedHammer(this LinkedListNode<StockPrice> node)
        {
            if (node == null || node.Previous == null) return false;
            var candleType = node.GetCandleStickType();
            if (candleType == CandleStickTypes.GreenHollow)
            {
                if (Math.Abs(node.Value.ChangePercent ?? 0) < Math.Abs(node.Previous.Value.ChangePercent ?? 0))
                {
                    //The body of the candle is short  with a longer upper shadow
                    return node.Value.HasShortBody && node.Value.UpperShadow > node.Value.LowerShadow;
                }
            }

            return false;
        }

        /// <summary>
        /// As the name indicates, the Morning Star is a sign of hope and a new beginning in a gloomy downtrend. 
        /// The pattern consists of three candles: one short-bodied candle (called a doji or a spinning top) between a preceding long black candle and a succeeding long white one. 
        /// The color of the real body of the short candle can be either white or black, and there is no overlap between its body and that of the black candle before. 
        /// It shows that the selling pressure that was there the day before is now subsiding. 
        /// The third white candle overlaps with the body of the black candle and shows a renewed buyer pressure and a start of a bullish reversal, especially if confirmed by the higher volume.
        /// </summary>
        /// <param name="before"></param>
        /// <returns></returns>
        public static bool IsMorningStar(this LinkedListNode<StockPrice> node)
        {
            if (node.Previous != null && node.Next != null)
            {
                var candleType = node.GetCandleStickType();

                //preceding long black candle
                if (node.Previous != null && node.Previous.GetCandleStickType() == CandleStickTypes.RedFilled)
                {
                    //one short-bodied candle (called a doji or a spinning top)
                    if (node.Value.HasShortBody)
                    {
                        //The color of the real body of the short candle can be either white or black
                        //there is no overlap between its body and that of the black candle before. 
                        if (((candleType == CandleStickTypes.GreenHollow || candleType == CandleStickTypes.GreenHollow) && node.Previous.Value.Close > node.Value.Close)
                            || ((candleType == CandleStickTypes.RedHollow || candleType == CandleStickTypes.RedFilled) && node.Previous.Value.Close > node.Value.Open))
                        {
                            //succeeding long white one 
                            if (node.Next.GetCandleStickType() == CandleStickTypes.GreenHollow)
                                return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Similar to the engulfing pattern, the Piercing Line is a two-candle bullish reversal pattern, also occurring in downtrends. 
        /// The first long black candle is followed by a white candle that opens lower than the previous close. 
        /// Soon thereafter, the buying pressure pushes the price up halfway or more (preferably two-thirds of the way) into the real body of the black candle.
        /// </summary>
        /// <param name="before"></param>
        /// <returns></returns>
        public static bool IsPiercingLine(this LinkedListNode<StockPrice> node)
        {
            if (node == null || node.Previous == null) return false;
            var candleType = node.GetCandleStickType();
            if (candleType == CandleStickTypes.GreenHollow)
            {
                if (Math.Abs(node.Value.ChangePercent ?? 0) < Math.Abs(node.Previous.Value.ChangePercent ?? 0))
                {
                    if (!node.Value.HasShortBody)
                    {
                        return node.Value.Open < node.Previous.Value.Close;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// The Bullish Engulfing pattern is a two-candle reversal pattern. 
        /// The second candle completely ‘engulfs’ the real body of the first one, without regard to the length of the tail shadows. 
        /// The Bullish Engulfing pattern appears in a downtrend and is a combination of one dark candle followed by a larger hollow candle. 
        /// On the second day of the pattern, price opens lower than the previous low, yet buying pressure pushes the price up to a higher level than the previous high, culminating in an obvious win for the buyers. 
        /// It is advisable to enter a long position when the price moves higher than the high of the second engulfing candle—in other words when the downtrend reversal is confirmed.
        /// </summary>
        /// <param name="before"></param>
        /// <returns></returns>
        public static bool IsBullishEngulfing(this LinkedListNode<StockPrice> node)
        {
            if (node == null || node.Previous == null) return false;

            if (Math.Abs(node.Value.ChangePercent ?? 0) > Math.Abs(node.Previous.Value.ChangePercent ?? 0))
            {
                return node.Value.High > node.Previous.Value.High && node.Value.Low < node.Previous.Value.Low;
            }
            return false;
        }

        /// <summary>
        /// Most bullish reversal patterns require bullish confirmation. 
        /// In other words, they must be followed by an upside price move which can come as a long hollow candlestick or a gap up and be accompanied by high trading volume. 
        /// This confirmation should be observed within three days of the pattern.
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static decimal SubsequentVolume(this LinkedListNode<StockPrice> head)
        {
            //Before we jump in on the bullish reversal action
            //confirm the upward trend by watching it closely for the next few days. 
            var volume = 0m;
            var current = head?.Next;
            while (current != null)
            {
                var candleType = current.GetCandleStickType();
                if (candleType == CandleStickTypes.GreenFilled
                    || candleType == CandleStickTypes.GreenHollow)
                {
                    //The reversal must also be validated through the rise in the trading volume.
                    volume += current.Value.Volume ?? 0;
                }
                current = current.Next;
            }
            return volume;
        }

        public static CandleStickTypes GetCandleStickType(this LinkedListNode<StockPrice> node)
        {
            if (node == null || node.Previous != null)
            {
                // GreenHollow 
                //  close > prior close
                //  close > open
                if (node.Value.Close > node.Previous.Value.Close && node.Value.Close > node.Value.Open)
                    return CandleStickTypes.GreenHollow;

                // GreenFilled 
                //  close > prior close
                //  close < open
                if (node.Value.Close > node.Previous.Value.Close && node.Value.Close < node.Value.Open)
                    return CandleStickTypes.GreenFilled;


                // RedFilled
                // close < prior close
                // close < open
                if (node.Value.Close < node.Previous.Value.Close && node.Value.Close < node.Value.Open)
                    return CandleStickTypes.RedFilled;

                // RedHollow
                //  close < prior close
                //  close > open
                if (node.Value.Close < node.Previous.Value.Close && node.Value.Close > node.Value.Open)
                    return CandleStickTypes.RedHollow;
            }

            return CandleStickTypes.None;
        }
    }
}
