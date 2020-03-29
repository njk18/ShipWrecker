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
    public static class ClearBoard
    {
        [FunctionName("ClearBoard")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Clear a specific board given the gameID.");

            string name = req.Query["gameID"];
            Board.playerType playerType = (Board.playerType)Enum.Parse(typeof(Board.playerType), req.Query["playerType"]);
            Guid gameID = new Guid(req.Query["gameID"]);

            if (playerType == Board.playerType.playerOne)
            {
                playerType = Board.playerType.playerOne;
                AddShip.counters[gameID][0] = 0;
                Board.boards[gameID][0] = new Board(Board.boards[gameID][0].boardSize, playerType);
            }
            else if(playerType == Board.playerType.playerTwo)
            {
                AddShip.counters[gameID][1] = 0;
                playerType = Board.playerType.playerTwo;
                Board.boards[gameID][1] = new Board(Board.boards[gameID][1].boardSize, playerType);
            }

            return new OkObjectResult("Board cleared.");
        }
    }
}
