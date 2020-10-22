using board;
using System.Collections.Generic;

namespace chess
{
    class ChessMatch
    {
        public Board board { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.WHITE;
            finished = false;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            placePieces();
        }

        public void executeMovement(Position origin, Position destination)
        {
            Piece p = board.removePiece(origin);
            p.increaseMovementNumber();
            Piece capturedPiece = board.removePiece(destination);
            board.placeApiece(p, destination);

            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }
        }

        public void play(Position origin, Position destination)
        {
            executeMovement(origin, destination);
            turn++;
            changePlayer();
        }

        public void validateOriginPosition(Position pos)
        {
            if (board.piece(pos) == null)
            {
                throw new BoardException("Não existe peça na posição escolhida!");
            }

            if (currentPlayer != board.piece(pos).color)
            {
                throw new BoardException("A peça de origem nao é sua!");
            }

            if (!board.piece(pos).hasPossibleMovements())
            {
                throw new BoardException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validateDestinationPosition(Position origin, Position destination)
        {
            if (!board.piece(origin).canMoveToDestination(destination))
            {
                throw new BoardException("Posição de destino inválida!");
            }
        }


        private void changePlayer()
        {
            if (currentPlayer == Color.WHITE)
            {
                currentPlayer = Color.BLACK;
            }
            else
            {
                currentPlayer = Color.WHITE;
            }
        }

        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece x in captured)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> gamePieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece x in pieces)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(capturedPieces(color));
            return aux;
        }

        public void placeNewPiece(char column, int line, Piece piece)
        {
            board.placeApiece(piece, new PositionChess(column, line).toPosition());
            pieces.Add(piece);
        }

        private void placePieces()
        {
            placeNewPiece('c', 1, new Tower(board, Color.WHITE));
            placeNewPiece('c', 2, new Tower(board, Color.WHITE));
            placeNewPiece('d', 2, new Tower(board, Color.WHITE));
            placeNewPiece('e', 2, new Tower(board, Color.WHITE));
            placeNewPiece('e', 1, new Tower(board, Color.WHITE));
            placeNewPiece('d', 1, new King(board, Color.WHITE));

            placeNewPiece('c', 7, new Tower(board, Color.BLACK));
            placeNewPiece('c', 8, new Tower(board, Color.BLACK));
            placeNewPiece('d', 7, new Tower(board, Color.BLACK));
            placeNewPiece('e', 7, new Tower(board, Color.BLACK));
            placeNewPiece('e', 8, new Tower(board, Color.BLACK));
            placeNewPiece('d', 8, new King(board, Color.BLACK));

        }
    }
}
