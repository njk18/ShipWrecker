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
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("The player has hit a cell");
            FireResponse response = new FireResponse();
            Guid gameID = Guid.Parse(req.Query["gameID"]);
            Board.playerType playerType = (Board.playerType)System.Enum.Parse(typeof(Board.playerType), req.Query["playerType"]);
            int xPosition = Int32.Parse(req.Query["xPosition"]);
            int yPosition = Int32.Parse(req.Query["yPosition"]);
            int boardSize = Board.boards[gameID][0].boardSize;
            
            Board currentBoard = null;
            if (GetGameID.playerTurn != playerType) {
                response.keepTurn = true;
                // Return a JSON response
                var fireRResponse = JsonConvert.SerializeObject(response, Formatting.Indented);
                return (ActionResult)new OkObjectResult(fireRResponse);
            }
            if (playerType == Board.playerType.playerOne )
            {
                currentBoard = Board.boards[gameID][1];

            } else if (playerType == Board.playerType.playerTwo)
            {
                currentBoard = Board.boards[gameID][0];
            }

            response.keepTurn = true;

            switch (currentBoard.getBattleGround()[xPosition, yPosition].shipState)
            {
                case Ship.ShipState.noShip:
                    {
                        currentBoard.getBattleGround()[xPosition, yPosition].shipState = Ship.ShipState.shipMiss;
                        response.state = Ship.ShipState.shipMiss ;
                        response.keepTurn = false;
                        response.wonGame = wonGame(currentBoard);
                        
                        break;
                    }
                   
                case Ship.ShipState.ship:
                    {
                        currentBoard.getBattleGround()[xPosition, yPosition].shipState = Ship.ShipState.shipHit;
                        response.state = Ship.ShipState.shipHit;
                        response.keepTurn = false;
                        response.wonGame = wonGame(currentBoard);
                        break;
                    }

                case Ship.ShipState.shipHit:
                    {
                        /* This ship is already hit, the click is redundant. To get to this case,
                         * the user must have clicked on the tile of a ship he's already hit.
                         * 
                         */
                        response.state = Ship.ShipState.shipHit;
                        response.keepTurn = true;
                        response.wonGame = wonGame(currentBoard);
                        break;
                    }
                case Ship.ShipState.shipMiss:
                    {
                        // Return miss, you hit water!
                        response.state = Ship.ShipState.shipMiss;
                        response.keepTurn = true;
                        response.wonGame = wonGame(currentBoard);
                        break;
                    }

            }
            if (GetGameID.playerTurn == Board.playerType.playerOne && response.keepTurn == false)
            {
                GetGameID.playerTurn = Board.playerType.playerTwo;

            } else if(GetGameID.playerTurn == Board.playerType.playerTwo && response.keepTurn == false)
            {
                GetGameID.playerTurn = Board.playerType.playerOne;
            }
            
            // Return a JSON response
            var fireResponse = JsonConvert.SerializeObject(response, Formatting.Indented);
            return (ActionResult)new OkObjectResult(fireResponse);
        }

        private static bool wonGame(Board currentBoard)
        {
           for(int i = 0; i < currentBoard.boardSize; i++)
            {
                for(int j = 0; j < currentBoard.boardSize; j++)
                {
                    if (currentBoard.getBattleGround()[i, j].shipState == Ship.ShipState.ship)
                        return false;
                }
           
            }
            return true;
        }
    }
}
