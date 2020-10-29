using board;

namespace Chess_Console.chess
{
    class Horse : Piece
    {
        public Horse(Board board, Color color)
            : base(board, color)
        {
        }

        private bool canMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] possibleMovements()
        {
            bool[,] mat = new bool[board.lines, board.columns];

            Position pos = new Position(0, 0);

            pos.defineValues(position.line - 1, position.column - 2);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            pos.defineValues(position.line - 2, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            pos.defineValues(position.line - 2, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            pos.defineValues(position.line - 1, position.column + 2);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            pos.defineValues(position.line + 1, position.column + 2);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            pos.defineValues(position.line + 2, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            pos.defineValues(position.line + 2, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            pos.defineValues(position.line + 1, position.column - 2);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            return mat;
        }

        public override string ToString()
        {
            return "H";
        }
    }
}
