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

        public static Board.playerType previousTurn = Board.playerType.playerTwo;
        public String playerTurn;
        public bool wonGame;
        public Ship.ShipState state { get; set; }

        public FireResponse(Board.playerType turn, Ship.ShipState state)
        {
            this.wonGame = false;
            this.state = state;

        }

        public FireResponse()
        {
            this.wonGame = false;
        }

        public void winGame()
        {
            this.wonGame = true;
        }


    }
}
