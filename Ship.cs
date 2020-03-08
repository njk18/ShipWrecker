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
        private int positionX { get; set; }
        private int positionY { get; set; }
        private ShipState shipState { get; set; }

        private enum ShipType
        {
            Carrier = 5,
            Battleship = 4,
            Cruiser = 3,
            Submarine = 2,
            Destroyer = 1
        }

        private enum ShipState
        {
            shipHit,
            shipMiss,
            noShip,
            ship
        }
  

        public Ship(bool shipRotation, string shipName, int x, int y, string stateType)
        {

            this.shipType = (ShipType)System.Enum.Parse(typeof(ShipType), shipName);
            this.shipRotation = shipRotation;
            this.positionX = x;
            this.positionY = y;
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

        public bool CheckShipPosition(int PositionX, int PositionY, bool shipRotation)
        {
            int boardSize = 8;
            for (int i = 0; i < shipSize; i++)
            {
                if (shipRotation) //horizontal
                {
                    if (PositionX + i >= boardSize)
                        return false;
                    else //if (board.getBattleGround()[PositionX + i, PositionY].getShipSate() != ShipState.noShip)
                        return false;
                }
                else
                {
                    if (PositionY + i >= boardSize)
                        return false;
                    else// if (board.getBattleGround()[PositionX, PositionY + i].getShipSate() != ShipState.noShip)
                        return false;
                }
            }
            return true;
        }



    }

}
