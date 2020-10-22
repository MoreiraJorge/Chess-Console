using board;

namespace chess
{
    class ChessMatch
    {
        public Board board { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
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

        public void play(Position origin, Position destination)
        {
            executeMovement(origin, destination);
            turn++;
            changePlayer();
        }

        public void validateOriginPosition(Position pos)
        {
            if(board.piece(pos) == null)
            {
                throw new BoardException("Não existe peça na posição escolhida!");
            }

            if(currentPlayer != board.piece(pos).color)
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
            if(currentPlayer == Color.WHITE)
            {
                currentPlayer = Color.BLACK;
            } else
            {
                currentPlayer = Color.WHITE;
            }
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
