using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TradingTools.Models.Stocks;
using TradingTools.Shared.Helpers;
using TradingTools.Shared.Services;

namespace TradingTools.FunctionApp.TradingPatterns
{
    public static class TradingPatternFunctions
    {
        [FunctionName("AddTradingPattern")]
        public static IActionResult AddSymbolToWatchList(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            [CosmosDB(CosmosDbConstants.TradingPatterns, CosmosDbConstants.Patterns, ConnectionStringSetting = CosmosDbConstants.ConnectionStringName)] out string docs,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            docs = requestBody;

            dynamic data = JsonConvert.DeserializeObject(requestBody);
            return new OkObjectResult(data);
        }

        [FunctionName("PatternsTrigger")]
        public static void SymbolsTrigger([CosmosDBTrigger(CosmosDbConstants.TradingPatterns, CosmosDbConstants.Patterns, ConnectionStringSetting = CosmosDbConstants.ConnectionStringName,
            LeaseCollectionName = "leases")]IReadOnlyList<Document> input, 
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);
            }
        }
    }
}
