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
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("HTTP request for a specific board.");

            Guid gameID = new Guid(req.Query["gameID"]);
            Board.playerType playerType = (Board.playerType)System.Enum.Parse(typeof(Board.playerType), req.Query["playerType"]);


            Board requestedBoard = null;

            if (playerType == Board.playerType.playerOne)
            {
                requestedBoard = Board.boards[gameID][0];
            }
            else if (playerType == Board.playerType.playerTwo)
            {
                requestedBoard = Board.boards[gameID][1];
            }

            var response = JsonConvert.SerializeObject(requestedBoard.getBattleGround(), Formatting.Indented);
            return new OkObjectResult(response);
        }
    }
}
