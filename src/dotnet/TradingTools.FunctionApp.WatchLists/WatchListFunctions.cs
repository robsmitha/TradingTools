using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace TradingTools.FunctionApp.WatchLists
{
    public static class WatchListFunctions
    {
        [FunctionName("SymbolsTrigger")]
        public static void SymbolsTrigger([CosmosDBTrigger(
            databaseName: "WatchLists",
            collectionName: "Symbols",
            ConnectionStringSetting = "DbConnection",
            LeaseCollectionName = "leases")]IReadOnlyList<Document> input, ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);
            }
        }
    }
}
