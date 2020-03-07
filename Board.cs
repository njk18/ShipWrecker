using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ShipWrecker
{

    class Board
    {
        // Map of all boards that are currently being used to play an instance of the game
        public static IDictionary<int, Board> boards = new Dictionary<int, Board>();
        Board board = new Board(8);

        private int boardSize;

        // Create board which is a 2D array of Tiles
        Ship[,] battleGround;

        public Board(int boardSize)
        {
            boardSize = 8;
            this.battleGround = new Ship[boardSize, boardSize];
        }

        public Ship[,] getBattleGround()
        {
            return this.battleGround;
        }

        public void check()
        {
            for(int i= 0; i < boardSize;i++)
                for(int j =0; j < boardSize; j++)
                {
                    battleGround[i,j] = new Ship(true, "carrier",i,j,"smthg");
                }
        }


    }
}
