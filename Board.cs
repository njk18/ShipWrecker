using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ShipWrecker
{

    /*  TODO
     *  
     *  - Handle adding ship on edge when shipSize > 1
     * 
     */

    class Board
    {
        // Map of all boards that are currently being used to play an instance of the game
        public static IDictionary<Guid, Board> boards = new Dictionary<Guid, Board>();


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
