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

            //testtttttttttttttttt
          
            string shipType = req.Query["shipType"];
            int xPosition = Int32.Parse(req.Query["xPosition"]);
            int yPosition = Int32.Parse(req.Query["yPosition"]);



        }
    }
}
