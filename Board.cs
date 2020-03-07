using System;
using System.Collections.Generic;
using System.Text;

namespace ShipWrecker
{

    class Board
    {

        private int boardSize;

        // Create board which is a 2D array of Tiles
        Tile[,] battleGround;

        public Board(int boardSize)
        {
            boardSize = 8;
            this.battleGround = new Tile[boardSize, boardSize];
        }


    }
}
