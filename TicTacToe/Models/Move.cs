namespace TicTacToe.Models
{
    public class Move
    {
        public int Id { get; set; }
        public string Position { get; set; }

        public Move(string position)
        {
            Position = position;
        }
    }
}