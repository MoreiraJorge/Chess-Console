using board;
using Chess_Console.chess;
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
        public bool xeque { get; private set; }
        public Piece enPassantVulnerable { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.WHITE;
            finished = false;
            xeque = false;
            enPassantVulnerable = null;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            placePieces();
        }

        public Piece executeMovement(Position origin, Position destination)
        {
            Piece p = board.removePiece(origin);
            p.increaseMovementNumber();
            Piece capturedPiece = board.removePiece(destination);
            board.placeApiece(p, destination);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }

            // #jogadaespecial roque pequeno
            if (p is King && destination.column == origin.column + 2)
            {
                Position originT = new Position(origin.line, origin.column + 3);
                Position destinastionT = new Position(origin.line, origin.column + 1);
                Piece T = board.removePiece(originT);
                T.increaseMovementNumber();
                board.placeApiece(T, destinastionT);
            }

            // #jogadaespecial roque grande
            if (p is King && destination.column == origin.column - 2)
            {
                Position originT = new Position(origin.line, origin.column - 4);
                Position destinastionT = new Position(origin.line, origin.column - 1);
                Piece T = board.removePiece(originT);
                T.increaseMovementNumber();
                board.placeApiece(T, destinastionT);
            }

            // #jogadaespecial en passant
            if (p is Pawn)
            {
                if (origin.column != destination.column && capturedPiece == null)
                {
                    Position posP;
                    if (p.color == Color.WHITE)
                    {
                        posP = new Position(destination.line + 1, destination.column);
                    }
                    else
                    {
                        posP = new Position(destination.line - 1, destination.column);
                    }
                    capturedPiece = board.removePiece(posP);
                    captured.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        public void undoMove(Position origin, Position destination, Piece capturedPiece)
        {
            Piece p = board.removePiece(destination);
            p.decreaseMovementNumber();
            if (capturedPiece != null)
            {
                board.placeApiece(capturedPiece, destination);
                captured.Remove(capturedPiece);
            }
            board.placeApiece(p, origin);

            // #jogadaespecial roque pequeno
            if (p is King && destination.column == origin.column + 2)
            {
                Position originT = new Position(origin.line, origin.column + 3);
                Position destinastionT = new Position(origin.line, origin.column + 1);
                Piece T = board.removePiece(destinastionT);
                T.decreaseMovementNumber();
                board.placeApiece(T, originT);
            }

            // #jogadaespecial roque grande
            if (p is King && destination.column == origin.column - 2)
            {
                Position originT = new Position(origin.line, origin.column - 4);
                Position destinastionT = new Position(origin.line, origin.column - 1);
                Piece T = board.removePiece(destinastionT);
                T.decreaseMovementNumber();
                board.placeApiece(T, originT);
            }

            // #jogadaespecial en passant
            if (p is Pawn)
            {
                if (origin.column != destination.column && capturedPiece == enPassantVulnerable)
                {
                    Piece peao = board.removePiece(destination);
                    Position posP;
                    if (p.color == Color.WHITE)
                    {
                        posP = new Position(3, destination.column);
                    }
                    else
                    {
                        posP = new Position(4, destination.column);
                    }
                    board.placeApiece(peao, posP);
                }
            }
        }

        public void play(Position origin, Position destination)
        {
            Piece capturedPiece = executeMovement(origin, destination);

            if (isInXeque(currentPlayer))
            {
                undoMove(origin, destination, capturedPiece);
                throw new BoardException("Não se pode colocar em xeque!");
            }

            Piece p = board.piece(destination);

            // #jogadaespecial promocao
            if (p is Pawn)
            {
                if ((p.color == Color.WHITE && destination.line == 0) || (p.color == Color.BLACK && destination.line == 7))
                {
                    p = board.removePiece(destination);
                    pieces.Remove(p);
                    Piece queen = new Queen(board, p.color);
                    board.placeApiece(queen, destination);
                    pieces.Add(queen);
                }
            }

            if (isInXeque(enemy(currentPlayer)))
            {
                xeque = true;
            } else
            {
                xeque = false;
            }

            if (testXequeMate(enemy(currentPlayer)))
            {
                finished = true;
            }
            else
            {
                turn++;
                changePlayer();
            }

            
            // #jogadaespecial en passant
            if (p is Pawn && (destination.line == origin.line - 2 || destination.line == origin.line + 2))
            {
                enPassantVulnerable = p;
            }
            else
            {
                enPassantVulnerable = null;
            }
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

        private Color enemy(Color color)
        {
            if (color == Color.WHITE)
            {
                return Color.BLACK;
            }
            else
            {
                return Color.WHITE;
            }
        }

        private Piece king(Color color)
        {
            foreach (Piece x in gamePieces(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool isInXeque(Color color)
        {
            Piece R = king(color);

            if (R == null)
            {
                throw new BoardException("Não tem rei da cor" + color + "no tabuleiro!");
            }

            foreach (Piece x in gamePieces(enemy(color)))
            {
                bool[,] mat = x.possibleMovements();
                if (mat[R.position.line, R.position.column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool testXequeMate(Color color)
        {
            if (!isInXeque(color))
            {
                return false;
            }

            foreach(Piece x in gamePieces(color))
            {
                bool[,] mat = x.possibleMovements();
                for(int i = 0; i < board.lines; i++)
                {
                    for (int j = 0; j < board.lines; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = executeMovement(origin, destination);
                            bool testXeque = isInXeque(color);
                            undoMove(origin, destination, capturedPiece);
                            if (!testXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void placeNewPiece(char column, int line, Piece piece)
        {
            board.placeApiece(piece, new PositionChess(column, line).toPosition());
            pieces.Add(piece);
        }

        private void placePieces()
        {
            placeNewPiece('a', 1, new Tower(board, Color.WHITE));
            placeNewPiece('b', 1, new Horse(board, Color.WHITE));
            placeNewPiece('c', 1, new Bishop(board, Color.WHITE));
            placeNewPiece('d', 1, new Queen(board, Color.WHITE));
            placeNewPiece('e', 1, new King(board, Color.WHITE, this));
            placeNewPiece('f', 1, new Bishop(board, Color.WHITE));
            placeNewPiece('g', 1, new Horse(board, Color.WHITE));
            placeNewPiece('h', 1, new Tower(board, Color.WHITE));
            placeNewPiece('a', 2, new Pawn(board, Color.WHITE, this));
            placeNewPiece('b', 2, new Pawn(board, Color.WHITE, this));
            placeNewPiece('c', 2, new Pawn(board, Color.WHITE, this));
            placeNewPiece('d', 2, new Pawn(board, Color.WHITE, this));
            placeNewPiece('e', 2, new Pawn(board, Color.WHITE, this));
            placeNewPiece('f', 2, new Pawn(board, Color.WHITE, this));
            placeNewPiece('g', 2, new Pawn(board, Color.WHITE, this));
            placeNewPiece('h', 2, new Pawn(board, Color.WHITE, this));

            placeNewPiece('a', 8, new Tower(board, Color.BLACK));
            placeNewPiece('b', 8, new Horse(board, Color.BLACK));
            placeNewPiece('c', 8, new Bishop(board, Color.BLACK));
            placeNewPiece('d', 8, new Queen(board, Color.BLACK));
            placeNewPiece('e', 8, new King(board, Color.BLACK, this));
            placeNewPiece('f', 8, new Bishop(board, Color.BLACK));
            placeNewPiece('g', 8, new Horse(board, Color.BLACK));
            placeNewPiece('h', 8, new Tower(board, Color.BLACK));
            placeNewPiece('a', 7, new Pawn(board, Color.BLACK, this));
            placeNewPiece('b', 7, new Pawn(board, Color.BLACK, this));
            placeNewPiece('c', 7, new Pawn(board, Color.BLACK, this));
            placeNewPiece('d', 7, new Pawn(board, Color.BLACK, this));
            placeNewPiece('e', 7, new Pawn(board, Color.BLACK, this));
            placeNewPiece('f', 7, new Pawn(board, Color.BLACK, this));
            placeNewPiece('g', 7, new Pawn(board, Color.BLACK, this));
            placeNewPiece('h', 7, new Pawn(board, Color.BLACK, this));

        }
    }
}
