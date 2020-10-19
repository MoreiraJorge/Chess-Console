using Chess_Console.board;

namespace Chess_Console.chess
{
    class King : Piece
    {
        public King(Board board, Color color) 
            : base(board, color)
        {
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
