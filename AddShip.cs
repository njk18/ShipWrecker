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

      //Test making change
        [FunctionName("addShip")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            log.LogInformation("HTTP request for AddShip.");

            Guid gameID = Guid.Parse(req.Query["gameID"]);
            string shipType = req.Query["shipType"];
            int xPosition = Int32.Parse(req.Query["x"]);
            int yPosition = Int32.Parse(req.Query["y"]);
            string stateType = req.Query["state"];
          
            // False = 0 : horizontal; True = 1 : vertical
            bool shipRotation = bool.Parse(req.Query["shipRotation"]);

            Ship s = new Ship(shipRotation, shipType, xPosition, yPosition, stateType);

          
            if(s.CheckShipPosition(xPosition,yPosition,shipRotation) == false)
                return null;
            else {
                if(shipRotation)
                {
                    for(int i = 0 ; i < s.shipSize ; i++)
                    {   
                     
                       Board.board[gameID].getBattleGround()[xPosition+1, yPosition] =  new Ship(shipRotation, shipSize, xPosition + i, yPosition,stateType);
                    
                    }
                }
                else{
                      for(int i = 0 ; i < s.shipSize ; i++)
                      {
                       
                        Board.board[gameID].getBattleGround()[xPosition, yPosition+1] = new Ship(shipRotation, shipSize, xPosition + i, yPosition,stateType);
                      }
                }
            }
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            return (ActionResult)new OkObjectResult(s);
          
        }
    }
}
