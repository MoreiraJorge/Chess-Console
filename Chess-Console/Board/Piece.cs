﻿namespace board
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int movementQuantity { get; protected set; }
        public Board board { get; protected set; }

        public Piece(Board board, Color color)
        {
            this.position = null;
            this.board = board;
            this.color = color;
            this.movementQuantity = 0;
        }

        public void increaseMovementNumber()
        {
            movementQuantity++;
        }

        public void decreaseMovementNumber()
        {
            movementQuantity--;
        }


        public bool hasPossibleMovements()
        {
            bool[,] mat = possibleMovements();
            for (int i = 0; i < board.lines; i++)
            {
                for (int j = 0; j < board.columns; j++)
                {
                    if(mat[i, j])
                    {
                        return true;
                    }

                }
            }
            return false;
        }

        public bool canMoveToDestination(Position pos)
        {
            return possibleMovements()[pos.line, pos.column];
        }

        public abstract bool[,] possibleMovements();

    }
}
