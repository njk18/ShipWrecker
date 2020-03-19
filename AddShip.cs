using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using Newtonsoft.Json;

namespace ShipWrecker
{
    public static class AddShip
    {

      //Test making change
        [FunctionName("addShip")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            log.LogInformation("HTTP request for AddShip.");

            Guid gameID = new Guid(req.Query["gameID"]);

            string shipType = req.Query["shipType"];
            int xPosition = Int32.Parse(req.Query["x"]);
            int yPosition = Int32.Parse(req.Query["y"]);
           
          
            // False = 0 : horizontal; True = 1 : vertical
            bool shipRotation = bool.Parse(req.Query["shipRotation"]);

            // Prepare response container;
            AddShipResponse response = new AddShipResponse();

            Ship s = new Ship(shipRotation, shipType, xPosition, yPosition);

            if (CheckShipPosition(s.shipSize, xPosition, yPosition, shipRotation, gameID) == false)
            {
                response.addShipStatus = false;
                var failResponse = JsonConvert.SerializeObject(response, Formatting.Indented);
                return (ActionResult)new OkObjectResult(failResponse);
            }
            else
            {
                if (shipRotation)
                {
                    for (int i = 0; i < s.shipSize; i++)
                    {

                        Board.boards[gameID].getBattleGround()[xPosition + i, yPosition] = new Ship(shipRotation, shipType, xPosition + i, yPosition);

                    }
                }
                else
                {
                    for (int i = 0; i < s.shipSize; i++)
                    {

                        Board.boards[gameID].getBattleGround()[xPosition, yPosition + i] = new Ship(shipRotation, shipType, xPosition + i, yPosition);
                    }
                }


                response.addShipStatus = true;
                var successResponse = JsonConvert.SerializeObject(response, Formatting.Indented);
                return (ActionResult)new OkObjectResult(successResponse);
            }
            
        }

          public static bool CheckShipPosition(int shipSize, int xPosition, int yPosition, bool shipRotation,  Guid gameID)
          {

            Board currentBoard = Board.boards[gameID];

            
            for (int i = 0; i < shipSize; i++)
            {
                if (shipRotation) //horizontal
                {
                    if (xPosition + i >= currentBoard.boardSize && yPosition >= currentBoard.boardSize)
                        return false;
                    else if (currentBoard.getBattleGround()[xPosition + i, yPosition].shipState != Ship.ShipState.noShip)
                        return false;
                }
                else
                {
                    if (yPosition + i >= currentBoard.boardSize && xPosition >= currentBoard.boardSize)
                        return false;
                    else if (currentBoard.getBattleGround()[xPosition, yPosition + i].shipState != Ship.ShipState.noShip)
                        return false;
                }
            }
            return true;
        }
    }
}
