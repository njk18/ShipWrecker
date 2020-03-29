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
using System.Collections.Generic;

namespace ShipWrecker
{
    public static class AddShip
    {

        // 0: countPlayerOneShipAdded, 1: countPlayerTwoShipAdded
        public static IDictionary<Guid, int[]> counters = new Dictionary<Guid, int[]>();

        //Test making change
        [FunctionName("addShip")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            log.LogInformation("HTTP request for AddShip.");

            Guid gameID = new Guid(req.Query["gameID"]);
            Board.playerType playerType = (Board.playerType) Enum.Parse(typeof(Board.playerType), req.Query["playerType"]);
            string shipType = req.Query["shipType"];
            int xPosition = Int32.Parse(req.Query["x"]);
            int yPosition = Int32.Parse(req.Query["y"]);


            // False = 0 : horizontal; True = 1 : vertical
            bool shipRotation = bool.Parse(req.Query["shipRotation"]);

            // Prepare response container;
            AddShipResponse response = new AddShipResponse();

            Ship s = new Ship(shipRotation, shipType, xPosition, yPosition);

            if (CheckShipPosition(playerType, s.shipSize, xPosition, yPosition, shipRotation, gameID) == false)
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
                        Board currentBoard = null;

                        if (playerType == Board.playerType.playerOne)
                        {
                            currentBoard = Board.boards[gameID][0];
                        }
                        else if(playerType == Board.playerType.playerTwo)
                        {
                            currentBoard = Board.boards[gameID][1];
                        }

                        currentBoard.getBattleGround()[xPosition + i, yPosition] = new Ship(shipRotation, shipType, xPosition + i, yPosition);
                      
                    }
                  
                }
                else
                {
                    for (int i = 0; i < s.shipSize; i++)
                    {
                        Board currentBoard = null;

                        if (playerType == Board.playerType.playerOne)
                        {
                            currentBoard = Board.boards[gameID][0];
                        }
                        else if (playerType == Board.playerType.playerTwo)
                        {
                            currentBoard = Board.boards[gameID][1];
                        }


                        currentBoard.getBattleGround()[xPosition, yPosition + i] = new Ship(shipRotation, shipType, xPosition, yPosition + i);
                      
                    }
                   
                }
                if (playerType == Board.playerType.playerOne)
                {
                    counters[gameID][0] += 1;
                }
                else if (playerType == Board.playerType.playerTwo)
                {
                    counters[gameID][1] += 1;
                }

                response.addShipStatus = true;
                var successResponse = JsonConvert.SerializeObject(response, Formatting.Indented);
                return (ActionResult)new OkObjectResult(successResponse);
            }

        }

        public static bool CheckShipPosition(Board.playerType playerType, int shipSize, int xPosition, int yPosition, bool shipRotation, Guid gameID)
        {
            Board currentBoard = null;

            if (playerType == Board.playerType.playerOne)
            {
                currentBoard = Board.boards[gameID][0];
            }
            else if (playerType == Board.playerType.playerTwo)
            {
                currentBoard = Board.boards[gameID][1];
            }


            for (int i = 0; i < shipSize; i++)
            {
                if (shipRotation) // Horizontal
                {
                    if (xPosition + shipSize - 1 >= currentBoard.boardSize || yPosition >= currentBoard.boardSize)
                        return false;
                    else if (currentBoard.getBattleGround()[xPosition + i, yPosition].shipState != Ship.ShipState.noShip)
                        return false;
                }
                else // Vertical
                {
                    if (yPosition + shipSize - 1 >= currentBoard.boardSize || xPosition >= currentBoard.boardSize)
                        return false;
                    else if (currentBoard.getBattleGround()[xPosition, yPosition + i].shipState != Ship.ShipState.noShip)
                        return false;
                }
            }

            return true;
        }
    }
}
