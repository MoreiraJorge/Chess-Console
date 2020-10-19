using System;

namespace Chess_Console.board
{
    class BoardException : Exception
    {
        public BoardException(string msg) : base(msg)
        {
        }
    }
}
