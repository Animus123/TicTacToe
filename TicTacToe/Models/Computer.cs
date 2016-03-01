using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToe.Models
{
    public class Computer
    {
        private Board Board { get; set; }
        public Board.Tile ComputerTile { get; private set; }

        public Computer(Board board, Board.Tile computerTile)
        {
            Board = board;
            ComputerTile = computerTile;
        }

        internal int[] Move()
        {
            int[] computerMove;
            //проверить может ли проиграть или выиграть возвращает true когда компьютер уже сделал ход
            computerMove = CheckIfComputerCanWinOrLoseNextMove();
            if (computerMove != null) return computerMove;
            //проверяет занята ли центральная клетка возвращает true когда компьютер сделал ход
            computerMove = CheckIfCenterIsEmpty();
            if (computerMove != null) return computerMove;
            //проверяет заняты ли углы возвращает true когда компьютер сделал ход
            computerMove = CheckIfCornersAreEmpty();
            if (computerMove != null) return computerMove;
            //вынуждает компьютер сделать хоть какой-нибудь ход
            computerMove = DoAnyMove();
            if (computerMove != null) return computerMove;
            return null;
        }

        private int[] DoAnyMove()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Board.Tiles[i, j] == Board.Tile.Emptу)
                    {
                        Board.Tiles[i, j] = ComputerTile;
                        return new int[] {i, j};
                    }
                }
            }
            return null;
        }

        internal void ChangeSide(Board.Tile computerTile)
        {
            ComputerTile = computerTile;
        }

        private int[] CheckIfCornersAreEmpty()
        {
            if (Board.Tiles[0, 0] == Board.Tile.Emptу)
            {
                Board.Tiles[0, 0] = ComputerTile;
                return new int[] { 0, 0 };
            }
            else if (Board.Tiles[2, 2] == Board.Tile.Emptу)
            {
                Board.Tiles[2, 2] = ComputerTile;
                return new int[] { 2, 2 };
            }
            else if (Board.Tiles[0, 2] == Board.Tile.Emptу)
            {
                Board.Tiles[0, 2] = ComputerTile;
                return new int[] { 0, 2 };
            }
            else if (Board.Tiles[2, 0] == Board.Tile.Emptу)
            {
                Board.Tiles[2, 0] = ComputerTile;
                return new int[] { 2, 0 };
            }
            return null;
        }

        private int[] CheckIfCenterIsEmpty()
        {
            if (Board.Tiles[1, 1] == Board.Tile.Emptу)
            {
                Board.Tiles[1, 1] = ComputerTile;
                return new int[] { 1, 1 };
            }
            return null;
        }

        private int[] CheckIfComputerCanWinOrLoseNextMove()
        {
            //проверить горизонтали
            int howManyPlayersTiles = 0;
            int howManyComputerTiles = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Board.Tiles[i, j] == Board.PlayerTile)
                    {
                        howManyPlayersTiles++;
                    }
                    else if (Board.Tiles[i, j] == ComputerTile)
                    {
                        howManyComputerTiles++;
                    }
                }
                if (howManyComputerTiles == 2)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Board.Tiles[i, j] == Board.Tile.Emptу)
                        {
                            Board.Tiles[i, j] = ComputerTile;
                            return new int[] { i, j};
                        }
                    }
                }
                else if (howManyPlayersTiles == 2)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Board.Tiles[i, j] == Board.Tile.Emptу)
                        {
                            Board.Tiles[i, j] = ComputerTile;
                            return new int[] { i, j };
                        }
                    }
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
                    if (Board.Tiles[j, i] == Board.PlayerTile)
                    {
                        howManyPlayersTiles++;
                    }
                    else if (Board.Tiles[i, j] == ComputerTile)
                    {
                        howManyComputerTiles++;
                    }
                }

                if (howManyComputerTiles == 2)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Board.Tiles[j, i] == Board.Tile.Emptу)
                        {
                            Board.Tiles[j, i] = ComputerTile;
                            return new int[] { j, i };
                        }
                    }
                }
                else if (howManyPlayersTiles == 2)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Board.Tiles[j, i] == Board.Tile.Emptу)
                        {
                            Board.Tiles[j, i] = ComputerTile;
                            return new int[] { j, i };
                        }
                    }
                }
                howManyPlayersTiles = 0;
                howManyComputerTiles = 0;
            }

            //проверить диагонали
            for (int i = 0; i < 3; i++)
            {
                if (Board.Tiles[i, i] == Board.PlayerTile)
                {
                    howManyPlayersTiles++;
                }
                else if (Board.Tiles[i, i] == ComputerTile)
                {
                    howManyComputerTiles++;
                }
            }

            if (howManyComputerTiles == 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Board.Tiles[i, i] == Board.Tile.Emptу)
                    {
                        Board.Tiles[i, i] = ComputerTile;
                        return new int[] { i, i };
                    }
                }
            }
            if (howManyPlayersTiles == 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Board.Tiles[i, i] == Board.Tile.Emptу)
                    {
                        Board.Tiles[i, i] = ComputerTile;
                        return new int[] { i, i };
                    }
                }
            }
            howManyPlayersTiles = 0;
            howManyComputerTiles = 0;

            for (int i = 0; i < 3; i++)
            {
                if (Board.Tiles[i, 2 - i] == Board.PlayerTile)
                {
                    howManyPlayersTiles++;
                }
                else if (Board.Tiles[i, 2 - i] == ComputerTile)
                {
                    howManyComputerTiles++;
                }
            }

            if (howManyComputerTiles == 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Board.Tiles[i, 2 - i] == Board.Tile.Emptу)
                    {
                        Board.Tiles[i, 2 - i] = ComputerTile;
                        return new int[] { i, 2-i};
                    }
                }
            }
            if (howManyPlayersTiles == 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Board.Tiles[i, 2 - i] == Board.Tile.Emptу)
                    {
                        Board.Tiles[i, 2 - i] = ComputerTile;
                        return new int[] { i, 2 - i };
                    }
                }
            }

            //ход не сделан
            return null;
        }
    }
}