using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ShipWrecker
{
    public static class WaitTurn
    {
        [FunctionName("WaitTurn")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Block till it is the requestin player's turn.");
            Guid gameID = Guid.Parse(req.Query["gameID"]);
            Board.playerType playerType = (Board.playerType)System.Enum.Parse(typeof(Board.playerType), req.Query["playerType"]);

            // Block till the previousTurn changes
            while (FireResponse.previousTurn[gameID] == playerType) { };

            return new OkObjectResult(true);
        }
    }
}
