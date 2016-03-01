using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicTacToe.Models
{
    public class Board
    {
        public int Id { get; set; }

        /// <summary>
        /// Определяет за кого игрок 'x' или 'o'
        /// </summary>
        public Tile PlayerTile { get; private set; }

        /// <summary>
        /// Все клетки поля
        /// </summary>
        public Tile[,] Tiles { get; private set; }
        public Computer Computer { get; set; }

        public Board(Board.Tile playerTile)
        {
            if (playerTile == Tile.Cross)
            {
                PlayerTile = Tile.Cross;
                Computer = new Computer(this, Tile.Noughts);
            }
            else
            {
                PlayerTile = Tile.Noughts;
                Computer = new Computer(this, Tile.Cross);
            }
            Tiles = new Tile[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Tiles[i, j] = Tile.Emptу;
                }
            }
        }

        /// <summary>
        /// Значения клеток на игровом поле
        /// </summary>
        public enum Tile
        {
            Noughts,
            Cross,
            Emptу
        }

        /// <summary>
        /// Сделать ход игроком
        /// </summary>
        /// <param name="index">Клетка в которую будет сделан ход</param>
        /// <returns>Сделан ли ход</returns>
        internal bool Move(int[] index)
        {
            if (Tiles[index[0], index[1]] == Tile.Emptу)
            {
                Tiles[index[0], index[1]] = PlayerTile;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Проверка, победил ли кто то из игроков
        /// </summary>
        internal Tile CheckWinner()
        {
            //проверить горизонтали
            int howManyPlayersTiles = 0;
            int howManyComputerTiles = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Tiles[i, j] == PlayerTile)
                    {
                        howManyPlayersTiles++;
                    }
                    else if (Tiles[i, j] == Computer.ComputerTile)
                    {
                        howManyComputerTiles++;
                    }
                }
                if (howManyComputerTiles == 3)
                {
                    return Computer.ComputerTile;
                }
                else if (howManyPlayersTiles == 3)
                {
                    return PlayerTile;
                }
                howManyPlayersTiles = 0;
                howManyComputerTiles = 0;
            }

            //проверить вертикали
            howManyPlayersTiles = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Tiles[j, i] == PlayerTile)
                    {
                        howManyPlayersTiles++;
                    }
                    else if (Tiles[i, j] == Computer.ComputerTile)
                    {
                        howManyComputerTiles++;
                    }
                }

                if (howManyComputerTiles == 3)
                {
                    return Computer.ComputerTile;
                }
                else if (howManyPlayersTiles == 3)
                {
                    return PlayerTile;
                }
                howManyPlayersTiles = 0;
                howManyComputerTiles = 0;
            }

            //проверить диагонали
            for (int i = 0; i < 3; i++)
            {
                if (Tiles[i, i] == PlayerTile)
                {
                    howManyPlayersTiles++;
                }
                else if (Tiles[i, i] == Computer.ComputerTile)
                {
                    howManyComputerTiles++;
                }
            }

            if (howManyComputerTiles == 3)
            {
                return Computer.ComputerTile;
            }
            if (howManyPlayersTiles == 3)
            {
                return PlayerTile;
            }
            howManyPlayersTiles = 0;
            howManyComputerTiles = 0;

            if ((Tiles[0, 2] == Computer.ComputerTile && Tiles[1, 1] == Computer.ComputerTile &&
                Tiles[2, 0] == Computer.ComputerTile))
            {
                return Computer.ComputerTile;
            }
            if ((Tiles[0, 2] == PlayerTile && Tiles[1, 1] == PlayerTile && Tiles[2, 0] == PlayerTile))
            {
                return PlayerTile;
            }

            //никто пока не победил
            return Tile.Emptу;
        }

        /// <summary>
        /// Есть ли еще пустые поля для ходов 
        /// </summary>
        internal bool DoesBoardHasEmptyTiles()
        {
            foreach (Tile tile in Tiles)
            {
                if (tile == Tile.Emptу)
                {
                    return true;
                }
            }
            return false;
        }
    }
}