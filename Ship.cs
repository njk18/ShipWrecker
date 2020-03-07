using System;
using System.Collections.Generic;
using System.Text;

namespace ShipWrecker
{

    public class Ship : Tile {

        private ShipType shipType;

        private enum ShipType
        {
            Carrier,
            BattleShip
        }

        public Ship(String shipName)
        {
            
            this.shipType = (ShipType)System.Enum.Parse(typeof(Tile), shipName);
        }

    }

}
