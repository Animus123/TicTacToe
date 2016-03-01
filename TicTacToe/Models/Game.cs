using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TicTacToe.Models
{
    public class Game
    {
        public int Id { get; set; }

        /// <summary>
        /// Победитель игры
        /// </summary>
        public Board.Tile Winner { get; set; } = Board.Tile.Emptу;

        public Player Player { get; set; }

        public Board Board { get; set; }

        /// <summary>
        /// Список всех совершенных в ходе игры ходов
        /// </summary>
        public List<Move> Moves { get; set; } = new List<Move>();
    }
}