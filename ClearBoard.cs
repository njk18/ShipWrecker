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
            Board.playerType playerType;
            Guid gameID = new Guid(req.Query["gameID"]);
            if (req.Query["playerType"].Equals("playerOne"))
            {
                playerType = Board.playerType.playerOne;
                Board.boards[gameID][0] = new Board(Board.boards[gameID][0].boardSize, playerType);
            }
            else
            {
                playerType = Board.playerType.playerTwo;
                Board.boards[gameID][1] = new Board(Board.boards[gameID][1].boardSize, playerType);
            }

            return new OkObjectResult("Board cleared.");
        }
    }
}
