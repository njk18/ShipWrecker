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

        private static Guid gameSessionID;
        private static Board[] sessionBoards = new Board[2];

        [FunctionName("GetGameID")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Request for game session ID.");

            int boardSize = Int32.Parse(req.Query["boardSize"]);

            if (sessionBoards == null || sessionBoards.Length == 0)
            {
                sessionBoards[0] = new Board(boardSize);
                gameSessionID = Guid.NewGuid();

            }
            else if(sessionBoards.Length == 1 && boardSize == sessionBoards[0].boardSize)
            {

                sessionBoards[1] = new Board(boardSize);
                Board.boards.Add(gameSessionID, sessionBoards);

            }
            else if(sessionBoards.Length == 1 && boardSize != sessionBoards[0].boardSize)
            {
                // Not gonna happen for now
            } else
            {
                gameSessionID = Guid.NewGuid();
            }



            var response = JsonConvert.SerializeObject(gameSessionID);

            return new OkObjectResult(response);
        }
    }
}
