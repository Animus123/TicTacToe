using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToe.Models
{
    public class Game
    {
        public Player Player { get; set; }

        public Board Board { get; set; }
    }
}