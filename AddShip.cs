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

            log.LogInformation("HTTP request for AddShip.");

            Guid gameID = Guid.Parse(req.Query["gameID"]);
            string shipSize = req.Query["shipType"];
            int PositionX = Int32.Parse(req.Query["x"]);
            int PositionY = Int32.Parse(req.Query["y"]);
            string stateType = req.Query["state"];
          
            // False = 0 : horizontal; True = 1 : vertical
            bool shipRotation = bool.Parse(req.Query["shipRotation"]);

            Ship s = new Ship(shipRotation, shipSize, PositionX, PositionY, stateType);

          
            if(s.CheckShipPosition(PositionX,PositionY,shipRotation) == false)
                return null;
            else {
                if(shipRotation)
                {
                    for(int i = 0 ; i < s.shipSize ; i++)
                    {    s = new Ship(shipRotation, shipSize, PositionX + i, PositionY,stateType);
           //   Board.board.getBattleGround()[PositionX+1, PostionY] = new Ship(true, "carrier", PositionX, PostionY, "smthg");
                    
                    }
                }
                else{
                      for(int i = 0 ; i < s.shipSize ; i++)
                      {
                       s = new Ship(shipRotation, shipSize, PositionX + i, PositionY,stateType);
           //   Board.board.getBattleGround()[PositionX, PostionY+1] = new Ship(true, "carrier", PositionX, PostionY, "smthg");
                      }
                }
            }
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            return (ActionResult)new OkObjectResult(s);// shipRotation + " " + shipSize + " " + x + " " + y);
          
        }
    }
}
