using Chess_Console.board;

namespace Chess_Console.chess
{
    class Tower : Piece
    {
        public Tower(Board board, Color color)
            : base(board, color)
        {
        }

        public override string ToString()
        {
            return "T";
        }
    }
}
