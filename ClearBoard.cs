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
    public static class ClearBoard
    {
        [FunctionName("ClearBoard")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Clear a specific board given the gameID.");

            string name = req.Query["gameID"];
            Guid gameID = new Guid(req.Query["gameID"]);


            Board.boards[gameID] = new Board(Board.boards[gameID].boardSize);

            return new OkObjectResult("Board cleared.");
        }
    }
}
