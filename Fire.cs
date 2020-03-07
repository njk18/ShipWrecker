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
    public static class Fire
    {
        [FunctionName("Fire")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("The player has hit a cell");

            Guid gameID = Guid.Parse(req.Query["gameID"]);
            int xPosition = Int32.Parse(req.Query["xPosition"]);
            int yPosition = Int32.Parse(req.Query["yPosition"]);
            int boardSize = Board.boards[gameID].getBoardSize();


            for (int x = 0; x < boardSize; x++)
            {
                for(int y = 0; y < boardSize; y++)
                {

                    if(Board.boards[gameID].getBattleGround()[x, y] == null) { 

                    }

                }
            }

                  //  Board.boards[gameID];


            return new OkObjectResult(responseMessage);
        }
    }
}
