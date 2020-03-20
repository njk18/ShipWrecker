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
    public static class GetBoard
    {
        [FunctionName("GetBoard")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("HTTP request for a specific board.");

            Guid gameID = new Guid(req.Query["gameID"]);
            Ship[,] requestedBoard = Board.boards[gameID].getBattleGround();

            int rowLength = requestedBoard.GetLength(0);
            int colLength = requestedBoard.GetLength(1);
            string boardArray = "";
            for (int i = 0; i < rowLength; i++) {

                for (int j = 0; j < colLength; j++)
                {
                    boardArray += string.Format("{0} ", requestedBoard[i, j]);
                }

                boardArray += System.Environment.NewLine + System.Environment.NewLine;

            }


            var response = JsonConvert.SerializeObject(boardArray, Formatting.Indented);
            return new OkObjectResult(response);
        }
    }
}
