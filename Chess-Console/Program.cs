using Chess_Console.board;
using System;

namespace Chess_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);
            Window.printBoard(board);
            Console.ReadLine();
        }
    }
}
