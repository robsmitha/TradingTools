using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TradingTools.Models.LUIS;
using TradingTools.Models.Twitter;
using TradingTools.Shared.Services;

namespace TradingTools.FunctionApp.TwitterAnalysis
{
    public static class TwitterAnalysisFunctions
    {
        public static readonly LUISClient luis = new LUISClient(
            predictionEndpoint: Environment.GetEnvironmentVariable("LUISApiEndpoint"),
            predictionKey: Environment.GetEnvironmentVariable("LUISApiPredictionKey"),
            appId: Environment.GetEnvironmentVariable("LUISAppID"),
            slotName: Environment.GetEnvironmentVariable("LUISSlotName"),
            version: Environment.GetEnvironmentVariable("LUISVersion"));

        public static readonly TwitterClient twitter = new TwitterClient(
               baseUrl: Environment.GetEnvironmentVariable("TwitterApiEndpoint"),
               version: Environment.GetEnvironmentVariable("TwitterApiVersion"),
               token: Environment.GetEnvironmentVariable("TwitterApiToken"));

        [FunctionName("AnalzeStockTweets")]
        public static void AnalzeStockTweets([TimerTrigger("0 0 */2 * * *")]TimerInfo myTimer, ILogger log)
        {
        }

        [FunctionName("GetUserTimeline")]
        public static async Task<IActionResult> GetUserTimeline(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetUserTimeline/{name}")] HttpRequest req,
            ILogger log,
            string name)
        {
            var tweets =  await twitter.SendAsync<List<Tweet>>("statuses/user_timeline.json",
                @params: new Dictionary<string, string> {
                    { "screen_name", name },
                    { "count", "100" },
                    { "exclude_replies", "true" },
                    { "include_rts", "false" }
                });
            var result = tweets;
            return await Task.FromResult(new OkObjectResult(result));
        }

        [FunctionName("GetTweetPrediction")]
        public static async Task<IActionResult> GetTweetPrediction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetTweetPrediction/{tweet}")] HttpRequest req,
            ILogger log,
            string tweet)
        {
            var prediction = await luis.SendAsync<Response>(tweet);
            var result = prediction;
            return new OkObjectResult(result);
        }
    }
}
