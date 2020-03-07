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

     
        [FunctionName("addShip")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            bool shipRotation = bool.Parse(req.Query["shipRotation"]);
            string shipSize = req.Query["shipType"];
            int PositionX = Int32.Parse(req.Query["x"]);
            int PostionY = Int32.Parse(req.Query["y"]);
            string stateType = req.Query["state"];
            Ship s = new Ship(shipRotation, shipSize, PositionX, PostionY,stateType);

         //   Board.board.getBattleGround()[PositionX, PostionY] = new Ship(true, "carrier", PositionX, PostionY, "smthg");




            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            return (ActionResult)new OkObjectResult(s);// shipRotation + " " + shipSize + " " + x + " " + y);
        }
    }
}
