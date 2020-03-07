using System;
using System.Collections.Generic;
using System.Text;

namespace ShipWrecker
{

    public class Ship  {

        private int shipSize;
        private ShipType shipType;
        private bool shipRotation;
        private int positionX;
        private int positionY;
        private ShipState shipState;

        private enum ShipType
        {
            Carrier,
            Battleship,
            Cruiser,
            Submarine,
            Destroyer
        }

        private enum ShipState
        {
           shipHit,
           shipMiss,
           noShip,
           ship
        }

        public Ship(bool shipRotation, string shipName, int x,int y, String stateType)
        {
            
            this.shipType = (ShipType)System.Enum.Parse(typeof(Ship), shipName);
            this.shipRotation = shipRotation;
            this.positionX = x;
            this.positionY = y;
            this.shipState = (ShipState)System.Enum.Parse(typeof(Ship), stateType);
            if (shipType.Equals(ShipType.Carrier)) shipSize = 2; 
        }

    }

}
