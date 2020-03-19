using System;
using System.Collections.Generic;
using System.Text;

namespace ShipWrecker
{

    public class Ship
    {
        //test change
        public int shipSize { get; set; }

        private ShipType shipType { get; set; }

        // False = 0 : horizontal; True = 1 : vertical
        public bool shipRotation { get; private set; }
        private int xPosition { get; set; }
        private int yPosition { get; set; }
        public ShipState shipState { get; set; }

        public enum ShipType
        {
            Carrier,
            Battleship,
            Cruiser ,
            Submarine ,
            Destroyer 
        }

        public enum ShipState
        {
            shipHit,
            shipMiss,
            noShip,
            ship
        }

        public Ship()
        {
            this.shipState = ShipState.noShip;
        }
  

        public Ship(bool shipRotation, string shipName, int x, int y)
        {
            this.shipType = (ShipType)System.Enum.Parse(typeof(ShipType), shipName);
            this.shipRotation = shipRotation;
            this.xPosition = x;
            this.yPosition = y;
            this.shipState = (ShipState)ShipState.ship;  

            switch (this.shipType)
            {
                case ShipType.Destroyer:
                    this.shipSize = 2;
                    break;
                case ShipType.Submarine:
                    this.shipSize = 3; // (int)ShipType.Submarine;
                    break;
                case ShipType.Cruiser:
                    this.shipSize = 3; // (int)ShipType.Cruiser;
                    break;
                case ShipType.Battleship:
                    this.shipSize = 4; // (int)ShipType.Battleship;
                    break;
                case ShipType.Carrier:
                    this.shipSize = 5; // (int)ShipType.Carrier;
                    break;
            }

        }

      



    }

}
