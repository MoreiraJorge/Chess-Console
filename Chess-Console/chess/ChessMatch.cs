using board;
using Microsoft.VisualBasic;
using System.Dynamic;

namespace chess
{
    class ChessMatch
    {
        public Board board { get; private set; }
        private int turn;
        private Color currentPlayer;
        public bool finished{ get; private set;}

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.WHITE;
            finished = false;
            placePieces();
        }

        public void executeMovement(Position origin, Position destination)
        {
            Piece p = board.removePiece(origin);
            p.increaseMovementNumber();
            Piece capturedPiece = board.removePiece(destination);
            board.placeApiece(p, destination);
        }

        private void placePieces()
        {
            board.placeApiece(new Tower(board, Color.WHITE), new PositionChess('c', 1).toPosition());
            board.placeApiece(new Tower(board, Color.WHITE), new PositionChess('c', 2).toPosition());
            board.placeApiece(new Tower(board, Color.WHITE), new PositionChess('d', 2).toPosition());
            board.placeApiece(new Tower(board, Color.WHITE), new PositionChess('e', 2).toPosition());
            board.placeApiece(new Tower(board, Color.WHITE), new PositionChess('e', 1).toPosition());
            board.placeApiece(new King(board, Color.WHITE), new PositionChess('d', 1).toPosition());

            board.placeApiece(new Tower(board, Color.BLACK), new PositionChess('c', 7).toPosition());
            board.placeApiece(new Tower(board, Color.BLACK), new PositionChess('c', 8).toPosition());
            board.placeApiece(new Tower(board, Color.BLACK), new PositionChess('d', 7).toPosition());
            board.placeApiece(new Tower(board, Color.BLACK), new PositionChess('e', 7).toPosition());
            board.placeApiece(new Tower(board, Color.BLACK), new PositionChess('e', 8).toPosition());
            board.placeApiece(new King(board, Color.BLACK), new PositionChess('d', 8).toPosition());
        }
    }
}
