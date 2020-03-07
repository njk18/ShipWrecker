using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ShipWrecker
{
    public static class AddShip
    {

        [FunctionName("AddShip")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("HTTP request for AddShip.");


            int gameID = Int32.Parse(req.Query["gameID"]);
            string shipType = req.Query["shipType"];
            int xPosition = Int32.Parse(req.Query["xPosition"]);
            int yPosition = Int32.Parse(req.Query["yPosition"]);
            int shipSize = Int32.Parse(req.Query["size"]);

            // False = 0 : horizontal; True = 1 : vertical
            bool alignment = Convert.ToBoolean(req.Query["alignment"]);


            if (shipSize == 1)
            {

                


            } else
            {
                if (!alignment)
                {
                    // horizontal


                }
                else
                {

                }
            }


        }
    }
}
