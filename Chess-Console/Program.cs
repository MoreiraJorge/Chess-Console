using board;
using chess;
using System;

namespace Chess_Console
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                ChessMatch match = new ChessMatch();

                while (!match.finished)
                {
                    try
                    {
                        Console.Clear();
                        Window.printMatch(match);


                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Position origin = Window.readChessPosition().toPosition();
                        match.validateOriginPosition(origin);

                        bool[,] possiblePositions = match.board.piece(origin).possibleMovements();

                        Console.Clear();
                        Window.printBoard(match.board, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Position destination = Window.readChessPosition().toPosition();
                        match.validateDestinationPosition(origin, destination);

                        match.play(origin, destination);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }


            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();

        }
    }
}
