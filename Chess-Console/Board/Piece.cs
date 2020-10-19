namespace Chess_Console.board
{
    class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int movementQuantity { get; protected set; }
        public Board board { get; protected set; }

        public Piece(Position position, Board board, Color color)
        {
            this.position = position;
            this.board = board;
            this.color = color;
            this.movementQuantity = 0;
        }
    }
}
