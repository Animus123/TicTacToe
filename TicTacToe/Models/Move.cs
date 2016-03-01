namespace TicTacToe.Models
{
    /// <summary>
    /// Информация о совершенном ходе
    /// </summary>
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