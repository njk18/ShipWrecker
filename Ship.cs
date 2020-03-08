using System;
using System.Collections.Generic;
using System.Text;

namespace ShipWrecker
{

    public class Ship
    {

        public int shipSize { get; private set; }

        private ShipType Type;

        public ShipType GetShipType()
        {
            return Type;
        }

        private void SetShipType(ShipType value)
        {
            Type = value;
        }

        public bool shipRotation { get; private set; }
        private int positionX { get; set; }
        private int positionY { get; set; }
        private ShipState shipState { get; set; }

        private enum ShipType
        {
            Carrier = 5,
            Battleship = 4,
            Cruiser = 3,
            Submarine = 3,
            Destroyer = 2
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

            this.Type = (ShipType)System.Enum.Parse(typeof(ShipType), shipName);
            this.shipRotation = shipRotation;
            this.positionX = x;
            this.positionY = y;
            this.shipState = (ShipState)System.Enum.Parse(typeof(ShipState), stateType);
            int shipSize = (int)Type;
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
