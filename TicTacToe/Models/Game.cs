using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToe.Models
{
    public class Game
    {
        public Board.Tile Winner { get; set; } = Board.Tile.Emptу;

        public Player Player { get; set; }

        public Board Board { get; set; }
    }
}