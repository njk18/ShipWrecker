using System;
using System.Collections.Generic;
using System.Text;

namespace ShipWrecker
{
    class GetGameIDResponse
    {

        public Guid gameSessionID { get; set; }
        public Board.playerType player { get; set; }


    }
}
