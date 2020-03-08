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
 
    public class FireResponse
    {
        public bool turn { get; set; }
        public Ship.ShipState state { get; set; }

        public FireResponse(bool turn, Ship.ShipState state)
        {
            this.turn = turn;
            this.state = state;
        }

        [FunctionName("Fire")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            bool turn = bool.Parse(req.Query["turn"]);
            Ship.ShipState state = Ship.ShipState.shipHit; //  Fire.state ; // req.Query[""];

           
            FireResponse responseState = new FireResponse(turn, state);

            


            // Return a JSON response
            var response = JsonConvert.SerializeObject(responseState, Formatting.Indented);
            return (ActionResult)new OkObjectResult(response);
        }

    }
}
