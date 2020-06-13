using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TradingTools.Shared.Helpers;

namespace TradingTools.FunctionApp.WatchLists
{
    public static class WatchListFunctions
    {
        [FunctionName("WatchListSymbolsTrigger")]
        public static void WatchListSymbolsTrigger([CosmosDBTrigger(CosmosDbConstants.DatabaseName, CosmosDbConstants.WatchListSymbols, 
            ConnectionStringSetting = CosmosDbConstants.ConnectionStringName, 
            LeaseCollectionName = CosmosDbConstants.LeaseCollectionName)] IReadOnlyList<Document> input,
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);
            }
        }

        [FunctionName("GetWatchListSymbols")]
        public static async Task<IActionResult> GetWatchListSymbols(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetWatchListSymbols/{UserID}")] HttpRequest req,
            [CosmosDB(CosmosDbConstants.DatabaseName, CosmosDbConstants.WatchListSymbols, 
            ConnectionStringSetting = CosmosDbConstants.ConnectionStringName,
            SqlQuery ="SELECT * FROM c WHERE c.UserID={UserID} ORDER BY c._ts DESC")] IEnumerable<dynamic> symbols,
            ILogger log,
            string UserID)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            if(symbols == null)
                return new StatusCodeResult(204);   //no content, for the user id

            return await Task.FromResult(new OkObjectResult(symbols));
        }

        [FunctionName("AddSymbolToWatchList")]
        public static IActionResult AddSymbolToWatchList(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, 
            [CosmosDB(CosmosDbConstants.DatabaseName, CosmosDbConstants.WatchListSymbols, 
            ConnectionStringSetting = CosmosDbConstants.ConnectionStringName)] out string docs,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            docs = requestBody;

            dynamic data = JsonConvert.DeserializeObject(requestBody);
            return new OkObjectResult(data);
        }
    }
}
