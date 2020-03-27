using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using Newtonsoft.Json;
using static ShipWrecker.Board;

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
            string player = req.Query["playerType"];
            string shipType = req.Query["shipType"];
            int xPosition = Int32.Parse(req.Query["x"]);
            int yPosition = Int32.Parse(req.Query["y"]);
           
          
            // False = 0 : horizontal; True = 1 : vertical
            bool shipRotation = bool.Parse(req.Query["shipRotation"]);

            // Prepare response container;
            AddShipResponse response = new AddShipResponse();

            Ship s = new Ship(shipRotation, shipType, xPosition, yPosition);

            if (CheckShipPosition(player,s.shipSize, xPosition, yPosition, shipRotation, gameID) == false)
            {
                Console.WriteLine("False Ship Position!");
                response.addShipStatus = false;
                var failResponse = JsonConvert.SerializeObject(response, Formatting.Indented);
                return (ActionResult)new OkObjectResult(failResponse);
            }
            else
            {
                Console.WriteLine("True Ship Position!");
                if (shipRotation)
                {
                    for (int i = 0; i < s.shipSize; i++)
                    {
                        if(player.Equals("playerOne"))
                        Board.boards[gameID][0].getBattleGround()[xPosition + i, yPosition] = new Ship(shipRotation, shipType, xPosition + i, yPosition);
                        else
                         Board.boards[gameID][1].getBattleGround()[xPosition + i, yPosition] = new Ship(shipRotation, shipType, xPosition + i, yPosition);


                    }
                }
                else
                {
                    for (int i = 0; i < s.shipSize; i++)
                    {
                        if (player.Equals("playerOne"))
                            Board.boards[gameID][0].getBattleGround()[xPosition, yPosition + i] = new Ship(shipRotation, shipType, xPosition , yPosition + i);
                        else
                            Board.boards[gameID][1].getBattleGround()[xPosition, yPosition + i] = new Ship(shipRotation, shipType, xPosition, yPosition + i);

                    }
                }


                response.addShipStatus = true;
                var successResponse = JsonConvert.SerializeObject(response, Formatting.Indented);
                return (ActionResult)new OkObjectResult(successResponse);
            }
            
        }

          public static bool CheckShipPosition(string player,int shipSize, int xPosition, int yPosition, bool shipRotation,  Guid gameID)
          {
            Board currentBoard;
            if (player.Equals("playerOne"))
                currentBoard = Board.boards[gameID][0];
            else currentBoard = Board.boards[gameID][1];

            for (int i = 0; i < shipSize; i++)
            {
                if (shipRotation) // Horizontal
                {
                    if (xPosition + shipSize -1>= currentBoard.boardSize || yPosition >= currentBoard.boardSize)
                        return false;
                    else if (currentBoard.getBattleGround()[xPosition + i, yPosition].shipState != Ship.ShipState.noShip)
                        return false;
                }
                else // Vertical
                {
                    if (yPosition + shipSize -1>= currentBoard.boardSize || xPosition >= currentBoard.boardSize)
                        return false;
                    else if (currentBoard.getBattleGround()[xPosition, yPosition + i].shipState != Ship.ShipState.noShip)
                        return false;
                }
            }
            return true;
            }
    }
}
