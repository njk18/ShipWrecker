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
        public bool keepTurn { get; set; }
        public Ship.ShipState state { get; set; }
        public bool wonGame { get; set; }

        public FireResponse(bool turn, Ship.ShipState state, bool wongame)
        {
            this.keepTurn = turn;
            this.state = state;
            this.wonGame = wongame;
        }

        public FireResponse()
        {
        }



    }
}
