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
   public class Fire
    {
        

        [FunctionName("Fire")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("The player has hit a cell");
            FireResponse response = new FireResponse();
            Guid gameID = Guid.Parse(req.Query["gameID"]);
            int xPosition = Int32.Parse(req.Query["xPosition"]);
            int yPosition = Int32.Parse(req.Query["yPosition"]);
            int boardSize = Board.boards[gameID].boardSize;

            Board currentBoard = Board.boards[gameID];

            response.keepTurn = true;

            switch (currentBoard.getBattleGround()[xPosition, yPosition].shipState)
            {
                case Ship.ShipState.noShip:
                    {
                        currentBoard.getBattleGround()[xPosition, yPosition].shipState = Ship.ShipState.shipMiss;
                        response.state = Ship.ShipState.shipMiss ;
                        response.keepTurn = false;

                        break;
                    }
                   
                case Ship.ShipState.ship:
                    {
                        currentBoard.getBattleGround()[xPosition, yPosition].shipState = Ship.ShipState.shipHit;
                        response.state = Ship.ShipState.shipHit;
                        response.keepTurn = false;

                        break;
                    }
                  
                case Ship.ShipState.shipHit:
                    /* This ship is already hit, the click is redundant. To get to this case,
                     * the user must have clicked on the tile of a ship he's already hit.
                     * 
                     */
                    response.state = Ship.ShipState.noShip;
                    response.keepTurn = true;

                    break;
                case Ship.ShipState.shipMiss:
                    // Return miss, you hit water!
                    response.state = Ship.ShipState.ship;
                    response.keepTurn = true;

                    break;
            }
            
            // Return a JSON response
            var fireResponse = JsonConvert.SerializeObject(response, Formatting.Indented);
            return (ActionResult)new OkObjectResult(fireResponse);
        }
    }
}
