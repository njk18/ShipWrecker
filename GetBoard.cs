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
    public static class GetBoard
    {
        [FunctionName("GetBoard")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("HTTP request for a specific board.");

            Guid gameID = new Guid(req.Query["gameID"]);
            Board requestedBoard = Board.boards[gameID];

            var response = JsonConvert.SerializeObject(requestedBoard.getBattleGround(), Formatting.Indented);
            return new OkObjectResult(response);
        }
    }
}
