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

            if (FireResponse.previousTurn != playerType || AddShip.countPlayerOneShipAdded != 5 || AddShip.countPlayerTwoShipAdded != 5) {
                // Return a JSON response
                var wrongPlayerResponse = JsonConvert.SerializeObject(response, Formatting.Indented);
                return (ActionResult)new OkObjectResult(wrongPlayerResponse);
            }

            if (playerType == Board.playerType.playerOne )
            {
                currentBoard = Board.boards[gameID][1];

            } else if (playerType == Board.playerType.playerTwo)
            {
                currentBoard = Board.boards[gameID][0];
            }


            switch (currentBoard.getBattleGround()[xPosition, yPosition].shipState)
            {
                case Ship.ShipState.noShip:
                    {
                        currentBoard.getBattleGround()[xPosition, yPosition].shipState = Ship.ShipState.shipMiss;
                        response.state = Ship.ShipState.shipMiss ;
                        response.wonGame = wonGame(currentBoard);
                        changeTurn(response);

                        break;
                    }
                   
                case Ship.ShipState.ship:
                    {
                        currentBoard.getBattleGround()[xPosition, yPosition].shipState = Ship.ShipState.shipHit;
                        response.state = Ship.ShipState.shipHit;
                        response.wonGame = wonGame(currentBoard);
                        changeTurn(response);

                        break;
                    }

                case Ship.ShipState.shipHit:
                    {
                        /* This ship is already hit, the click is redundant. To get to this case,
                         * the user must have clicked on the tile of a ship he's already hit.
                         * 
                         */
                        response.state = Ship.ShipState.shipHit;
                        response.wonGame = wonGame(currentBoard);
                        keepTurn(response);

                        break;
                    }
                case Ship.ShipState.shipMiss:
                    {
                        // Return miss, you hit water!
                        response.state = Ship.ShipState.shipMiss;
                        response.wonGame = wonGame(currentBoard);
                        keepTurn(response);

                        break;
                    }

            }   
            
            // Return a JSON response
            var fireResponse = JsonConvert.SerializeObject(response, Formatting.Indented);
            return (ActionResult)new OkObjectResult(fireResponse);
        }

        private static void changeTurn(FireResponse response)
        {

            String playerOne = Enum.GetName(typeof(Board.playerType), Board.playerType.playerOne);
            String playerTwo = Enum.GetName(typeof(Board.playerType), Board.playerType.playerTwo);

            if (FireResponse.previousTurn == Board.playerType.playerOne)
            {
                response.playerTurn = playerTwo;
                FireResponse.previousTurn = Board.playerType.playerTwo;
            }
            else if (FireResponse.previousTurn == Board.playerType.playerTwo)
            {
                response.playerTurn = playerOne;
                FireResponse.previousTurn = Board.playerType.playerOne;
            }
        }

        private static void keepTurn(FireResponse response)
        {
            String playerOne = Enum.GetName(typeof(Board.playerType), Board.playerType.playerOne);
            String playerTwo = Enum.GetName(typeof(Board.playerType), Board.playerType.playerTwo);

            if (FireResponse.previousTurn == Board.playerType.playerOne)
            {
                response.playerTurn = playerTwo;
                FireResponse.previousTurn = Board.playerType.playerTwo;
            }
            else if (FireResponse.previousTurn == Board.playerType.playerTwo)
            {
                response.playerTurn = playerOne;
                FireResponse.previousTurn = Board.playerType.playerOne;
            }
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
