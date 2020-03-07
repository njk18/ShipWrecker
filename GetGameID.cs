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
        [FunctionName("GetGameID")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Request for game session ID.");

            int boardSize = Int32.Parse(req.Query["boardSize"]);
            Guid gameSessionID = Guid.NewGuid();
            Board gameSessionBoard = new Board(boardSize);

            Board.boards.Add(gameSessionID, gameSessionBoard);

            var response = JsonConvert.SerializeObject(gameSessionID);

            return new OkObjectResult(response);
        }
    }
}
