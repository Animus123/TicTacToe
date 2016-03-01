using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicTacToe.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }

        public string Guid { get; set; }

        public Player()
        {
            Guid = System.Guid.NewGuid().ToString("d");
        }  
    }
}