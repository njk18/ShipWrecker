using System;
using System.Collections.Generic;
using System.Text;

namespace ShipWrecker
{

    public class Ship
    {
        //test change
        public int shipSize {
            get
            {
                return this.shipSize;
            }

            private set { this.shipSize = value; }
        }

        private ShipType shipType { get { return this.shipType; } set { this.shipType = value; } }

        // False = 0 : horizontal; True = 1 : vertical
        public bool shipRotation { get; private set; }
        private int xPosition { get; set; }
        private int yPosition { get; set; }
        public ShipState shipState { get; set; }

        public enum ShipType
        {
            Carrier = 5,
            Battleship = 4,
            Cruiser = 3,
            Submarine = 2,
            Destroyer = 1
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
  

        public Ship(bool shipRotation, string shipName, int x, int y, string stateType)
        {

            this.shipType = (ShipType)System.Enum.Parse(typeof(ShipType), shipName);
            this.shipRotation = shipRotation;
            this.xPosition = x;
            this.yPosition = y;
            this.shipState = (ShipState)System.Enum.Parse(typeof(ShipState), stateType);

            switch(this.shipType)
            {
                case ShipType.Destroyer:
                    this.shipSize = (int)ShipType.Destroyer;
                    break;
                case ShipType.Submarine:
                    this.shipSize = (int)ShipType.Submarine;
                    break;
                case ShipType.Cruiser:
                    this.shipSize = (int)ShipType.Cruiser;
                    break;
                case ShipType.Battleship:
                    this.shipSize = (int)ShipType.Battleship;
                    break;
                case ShipType.Carrier:
                    this.shipSize = (int)ShipType.Carrier;
                    break;
            }
        }

      



    }

}
