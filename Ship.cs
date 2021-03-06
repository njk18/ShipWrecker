﻿using System;
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
            carrier,
            battleship,
            cruiser,
            submarine,
            destroyer 
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
                case ShipType.destroyer:
                    this.shipSize = 2;
                    break;
                case ShipType.submarine:
                    this.shipSize = 3; 
                    break;
                case ShipType.cruiser:
                    this.shipSize = 3; 
                    break;
                case ShipType.battleship:
                    this.shipSize = 4; 
                    break;
                case ShipType.carrier:
                    this.shipSize = 5; 
                    break;
            }

        }

      



    }

}
