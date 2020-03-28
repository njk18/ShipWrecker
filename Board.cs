using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ShipWrecker
{

    public class Board
    {
        // Map of all boards that are currently being used to play an instance of the game  
        public static IDictionary<Guid, Board[]> boards = new Dictionary<Guid, Board[]>();
        public enum playerType { playerOne, playerTwo };
        public playerType player;
        public int boardSize { get; private set; }

        // Create board which is a 2D array of Tiles
        Ship[,] battleGround;

        public Board(int boardSize, playerType player)
        {
            this.boardSize = boardSize;
            this.battleGround = new Ship[boardSize, boardSize];
            this.player = player;
            createBoard();
        }

        public Ship[,] getBattleGround()
        {
            return this.battleGround;
        }

        public void createBoard()
        {
            for(int i= 0; i < boardSize;i++)
                for(int j =0; j < boardSize; j++)
                {
                    this.battleGround[i,j] = new Ship();
                }
        }

    }
}
