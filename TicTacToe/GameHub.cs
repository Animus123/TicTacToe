using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using TicTacToe.Models;

namespace TicTacToe
{
    public class GameHub : Hub
    {
        public void NewPlayer()
        {
            var player = GameState.Instance.CreatePlayer();
            var game = GameState.Instance.CreateGame(player);
            Clients.Caller.id = player.Guid;
        }

        public void ResetGame(string side)
        {
            var userId = Clients.Caller.id;
            var player = GameState.Instance.GetPlayer(userId);
            GameState.Instance.RemoveGame(player);
            if (side == "noughts")
            {
                GameState.Instance.CreateGame(player, Board.Tile.Noughts);
            }
            else
            {
                GameState.Instance.CreateGame(player, Board.Tile.Cross);         
            }

        }

        public bool Move(string tileName)
        {
            var userId = Clients.Caller.id;
            var player = GameState.Instance.GetPlayer(userId);
            var game = GameState.Instance.FindGame(player);

            if (game!=null && game.Winner == Board.Tile.Emptу)
            {
                //сделать ход и возвратить true если ход сделан
                int[] tileIndex = tileName.Where(char.IsDigit).Select(i => int.Parse(i.ToString())).ToArray();
                bool move = game.Board.Move(tileIndex);
                if (tileName!=null)
                {
                    game.Moves.Add(new Move("player: " + tileName)); 
                }

                game.Winner = game.Board.CheckWinner();

                if (game.Winner == game.Board.PlayerTile)
                {
                    Clients.Caller.playerWins();
                    using (var context = new GameContext())
                    {
                        context.Games.Add(game);
                        context.SaveChanges();
                    }
                    return true;
                }
                else if (!game.Board.DoesBoardHasEmptyTiles())
                {
                    Clients.Caller.tie();
                    using (var context = new GameContext())
                    {
                        context.Games.Add(game);
                        context.SaveChanges();
                    }
                }

                return true;
            }
            return false;       
        }

        public void ComputerMove()
        {
            var userId = Clients.Caller.id;
            var player = GameState.Instance.GetPlayer(userId);
            var game = GameState.Instance.FindGame(player);

            if (game.Winner == Board.Tile.Emptу)
            {
                int[] move = game.Board.Computer.Move();
                if (move!=null)
                {
                    game.Moves.Add(new Move("computer: [" + move[0] + "," + move[1] + "]")); 
                }
                Clients.Caller.computerMove(move);

                game.Winner = game.Board.CheckWinner();
                if (game.Winner == game.Board.Computer.ComputerTile)
                {
                    Clients.Caller.computerWins();
                    using (var context = new GameContext())
                    {
                        context.Games.Add(game);
                        context.SaveChanges();
                    }
                }
                else if (!game.Board.DoesBoardHasEmptyTiles())
                {
                    Clients.Caller.tie();
                    using (var context = new GameContext())
                    {
                        context.Games.Add(game);
                        context.SaveChanges();
                    }
                }
            }

        }
    }
}