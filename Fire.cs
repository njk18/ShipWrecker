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

            switch(Board.boards[gameID].getBattleGround()[xPosition, yPosition].shipState)
            {
                case Ship.ShipState.noShip:

                    break;
                case Ship.ShipState.ship:

                    break;
                case Ship.ShipState.shipHit:
                    /* This ship is already hit, the click is redundant. To get to this case,
                     * the user must have clicked on the tile of a ship he's already hit.
                     * 
                     */


                    break;
                case Ship.ShipState.shipMiss:
                    // Return miss, you hit water!

                    break;
            }


            return new OkObjectResult(responseMessage);
        }
    }
}
