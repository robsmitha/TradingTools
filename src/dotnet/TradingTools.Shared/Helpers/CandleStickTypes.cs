using System;
using System.Collections.Generic;
using System.Text;

namespace TradingTools.Shared.Helpers
{

    /// <summary>
    /// 
    /// Candlesticks are hollow (white) when the close is above the open and filled when the close is below the open.
    /// 
    /// Colored candlesticks are made up of four components in two groups. 
    ///     * First, a close lower than the prior close gets a red candlestick and a higher close gets a green candlestick. 
    ///     * Second, a candlestick is hollow when the close is above the open and filled when the close is below the open.
    /// 
    /// Each candlestick reflects the day's price action.
    ///     * Hollow candlesticks tell us that a security moved higher after its open.
    ///     * Filled candlestick indicates that a security moved lower after the open.
    /// </summary>
    public enum CandleStickTypes
    {
        /// <summary>
        /// Green filled candlesticks convey important information on price action (less popular).
        ///     * Even though the close was above the prior close (green), prices moved lower after the open (filled). 
        ///     * Despite closing higher on the day, there was some evidence of selling pressure.
        /// </summary>
        GreenFilled,

        /// <summary>
        /// Green hollow candlesticks are indicative of a strong uptrend (popular)
        /// </summary>
        GreenHollow,

        /// <summary>
        /// red filled candlesticks are indicative of a strong downtrend (popular)
        /// </summary>
        RedFilled,

        /// <summary>
        /// Red hollow candlesticks convey important information on price action (less popular)
        ///     * Even though the close is below the prior close (red), prices managed to move higher after the open (hollow). 
        ///     * Despite closing lower on the day, there was some evidence of buying pressure during the day.
        /// </summary>
        RedHollow,
        None
    }
}
