using board;
using chess;
using System;

namespace Chess_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            try
            {
                Board board = new Board(8, 8);

                board.placeApiece(new Tower(board, Color.BLACK), new Position(0, 0));
                board.placeApiece(new Tower(board, Color.BLACK), new Position(1, 3));
                board.placeApiece(new King(board, Color.BLACK), new Position(2, 4));
                Window.printBoard(board);

            } catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
            */

            PositionChess pos = new PositionChess('c', 7);
            Console.WriteLine(pos);
            Console.WriteLine(pos.toPosition());

            Console.ReadLine();
        }
    }
}
