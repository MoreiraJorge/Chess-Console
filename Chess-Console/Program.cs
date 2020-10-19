using Chess_Console.board;
using Chess_Console.chess;
using System;

namespace Chess_Console
{
    class Program
    {
        static void Main(string[] args)
        {

            Board board = new Board(8, 8);

            board.placeApiece(new Tower(board, Color.BLACK) , new Position(0,0));
            board.placeApiece(new Tower(board, Color.BLACK), new Position(1, 3));
            board.placeApiece(new King(board, Color.BLACK), new Position(2, 4));
            Window.printBoard(board);

            Console.ReadLine();
        }
    }
}
