using board;

namespace chess
{
    class King : Piece
    {
        private ChessMatch match;
        public King(Board board, Color color, ChessMatch match) 
            : base(board, color)
        {
            this.match = match;
        }

        private bool canMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }

        private bool checkTowerForRoque(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p is Tower && p.color == color && p.movementQuantity == 0;
        }

        public override bool[,] possibleMovements()
        {
            bool[,] mat = new bool[board.lines, board.columns];

            Position pos = new Position(0, 0);

            //acima
            pos.defineValues(position.line - 1, position.column);
            if(board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            //NE
            pos.defineValues(position.line - 1, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            //direita
            pos.defineValues(position.line, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            //SE
            pos.defineValues(position.line + 1, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            //abaixo
            pos.defineValues(position.line + 1, position.column);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            //SO
            pos.defineValues(position.line + 1, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            //esquerda
            pos.defineValues(position.line, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            //NO
            pos.defineValues(position.line - 1, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            // #jogadaespecial roque
            if (movementQuantity == 0 && !match.xeque)
            {
                // #jogadaespecial roque pequeno
                Position posT1 = new Position(position.line, position.column + 3);
                if (checkTowerForRoque(posT1))
                {
                    Position p1 = new Position(position.line, position.column + 1);
                    Position p2 = new Position(position.line, position.column + 2);
                    if (board.piece(p1) == null && board.piece(p2) == null)
                    {
                        mat[position.line, position.column + 2] = true;
                    }
                }
                // #jogadaespecial roque grande
                Position posT2 = new Position(position.line, position.column - 4);
                if (checkTowerForRoque(posT2))
                {
                    Position p1 = new Position(position.line, position.column - 1);
                    Position p2 = new Position(position.line, position.column - 2);
                    Position p3 = new Position(position.line, position.column - 3);
                    if (board.piece(p1) == null && board.piece(p2) == null && board.piece(p3) == null)
                    {
                        mat[position.line, position.column - 2] = true;
                    }
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
