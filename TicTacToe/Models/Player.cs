using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToe.Models
{
    public class Player
    {
        public Player()
        {
            Id = Guid.NewGuid().ToString("d");
        }
        public string Id { get; set; }
    }
}