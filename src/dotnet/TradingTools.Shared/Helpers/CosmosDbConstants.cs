using System;
using System.Collections.Generic;
using System.Text;

namespace TradingTools.Shared.Helpers
{
    public class CosmosDbConstants
    {
        /// <summary>
        /// A connection string value in your local.settings,json file
        /// </summary>
        public const string ConnectionStringName = "DbConnection";

        /// <summary>
        /// Database in cosmos db
        /// </summary>
        public const string DatabaseName = "TradingToolsDB";

        /// <summary>
        /// Collection for user watch list symbols
        /// </summary>
        public const string WatchListSymbols = "WatchListSymbols";

        /// <summary>
        /// Collection for storing candle stick pattern analysis
        /// </summary>
        public const string TradingPatterns = "TradingPatterns";

        /// <summary>
        /// Table for triggers. Don't delete or change this unless you know what you're doing.
        /// </summary>
        public const string LeaseCollectionName = "leases";
    }
}
