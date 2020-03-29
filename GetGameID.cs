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
    public static class GetGameID
    {

        private static Guid tempGameSessionID = Guid.Empty;

        [FunctionName("GetGameID")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Request for game session ID.");

            int boardSize = Int32.Parse(req.Query["boardSize"]);
            GetGameIDResponse gameIDResponse = new GetGameIDResponse();
            Board[] sessionBoards = new Board[2];
            // Because we cannot pass a static field, since it is pass by reference, all game sessions in the dictionary will have the latest generated Guid
            Guid gameSessionID = Guid.Empty;



            if (sessionBoards[0] == null)
            {

                sessionBoards[0] = new Board(boardSize, Board.playerType.playerOne);
                tempGameSessionID = Guid.NewGuid();

                // Prepare response
                gameIDResponse.gameSessionID = tempGameSessionID;
                gameIDResponse.player = Enum.GetName(typeof(Board.playerType) ,Board.playerType.playerOne);

            }
            else if(sessionBoards[0] != null && boardSize == sessionBoards[0].boardSize)
            {

                sessionBoards[1] = new Board(boardSize, Board.playerType.playerTwo);

                gameSessionID = tempGameSessionID;

                // Since we're passing by reference
                Board[] boards = { sessionBoards[0], sessionBoards[1] };

                // Now that we have two players, add the boards to the global boards Dictionary
                Board.boards.Add(gameSessionID, boards);
                // And we can now also initialize the previous turn to playerTwo, and the ship counters for each player to 0
                FireResponse.previousTurn.Add(gameSessionID, Board.playerType.playerTwo);
                int[] counts = { 0, 0 };
                AddShip.counters.Add(gameSessionID,  counts);

                // Prepare response
                gameIDResponse.gameSessionID = gameSessionID;
                gameIDResponse.player = Enum.GetName(typeof(Board.playerType), Board.playerType.playerTwo);

                // Clear the sessionBoard in order to create a new sessionBoard on the next call
                Array.Clear(sessionBoards, 0, sessionBoards.Length);

            }
            else if(sessionBoards[0] != null && boardSize != sessionBoards[0].boardSize)
            {
                // Not gonna happen for now
            }

            var response = JsonConvert.SerializeObject(gameIDResponse);

            return new OkObjectResult(response);
        }
    }
}
