using board;
using chess;
using System.Text.RegularExpressions;

namespace Chess_Console.chess
{
    class Pawn : Piece
    {
        private ChessMatch match;
        public Pawn(Board board, Color color, ChessMatch match)
            : base(board, color)
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool hasEnemy(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p.color != color;
        }

        private bool free(Position pos)
        {
            return board.piece(pos) == null;
        }

        public override bool[,] possibleMovements()
        {
            bool[,] mat = new bool[board.lines, board.columns];

            Position pos = new Position(0, 0);

            if (color == Color.WHITE)
            {
                pos.defineValues(position.line - 1, position.column);
                if (board.validPosition(pos) && free(pos))
                {
                    mat[pos.line, pos.column] = true;
                }

                pos.defineValues(position.line - 2, position.column);
                Position p2 = new Position(position.line - 1, position.column);
                if (board.validPosition(p2) && free(p2) && board.validPosition(pos) && free(pos) && movementQuantity == 0)
                {
                    mat[pos.line, pos.column] = true;
                }

                pos.defineValues(position.line - 1, position.column - 1);
                if (board.validPosition(pos) && hasEnemy(pos))
                {
                    mat[pos.line, pos.column] = true;
                }

                pos.defineValues(position.line - 1, position.column + 1);
                if (board.validPosition(pos) && hasEnemy(pos))
                {
                    mat[pos.line, pos.column] = true;
                }

                // #jogadaespecial en passant
                /*
                if (position.line == 3)
                {
                    Position left = new Position(position.line, position.column - 1);
                    if (board.validPosition(left) && hasEnemy(left) && board.piece(left) == match.vulneravelEnPassant)
                    {
                        mat[left.line - 1, left.column] = true;
                    }
                    Position right = new Position(position.line, position.column + 1);
                    if (board.validPosition(right) && hasEnemy(right) && board.piece(right) == match.vulneravelEnPassant)
                    {
                        mat[right.line - 1, right.column] = true;
                    }
                }
                */
            }
            else
            {
                pos.defineValues(position.line + 1, position.column);
                if (board.validPosition(pos) && free(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.defineValues(position.line + 2, position.column);
                Position p2 = new Position(position.line + 1, position.column);
                if (board.validPosition(p2) && free(p2) && board.validPosition(pos) && free(pos) && movementQuantity == 0)
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.defineValues(position.line + 1, position.column - 1);
                if (board.validPosition(pos) && hasEnemy(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.defineValues(position.line + 1, position.column + 1);
                if (board.validPosition(pos) && hasEnemy(pos))
                {
                    mat[pos.line, pos.column] = true;
                }

                // #jogadaespecial en passant
                /*
                if (position.line == 4)
                {
                    Position left = new Position(position.line, position.column - 1);
                    if (board.validPosition(left) && hasEnemy(left) && board.piece(left) == match.vulneravelEnPassant)
                    {
                        mat[left.line + 1, left.column] = true;
                    }
                    Position right = new Position(position.line, position.column + 1);
                    if (board.validPosition(right) && hasEnemy(right) && board.piece(right) == match.vulneravelEnPassant)
                    {
                        mat[right.line + 1, right.column] = true;
                    }
                }
                */
            }

            return mat;
        }
    }
}
