using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace TicTacToe
{
    public class GameHub : Hub
    {
        public void NewPlayer()
        {
            var player = GameState.Instance.CreaterPlayer();
            var game = GameState.Instance.CreateGame(player);
            Clients.Caller.id = player.Id;
        }

        public void ResetGame()
        {
            var userId = Clients.Caller.id;
            var player = GameState.Instance.GetPlayer(userId);
            GameState.Instance.RemoveGame(player);
            GameState.Instance.CreateGame(player);

        }

        public bool Move(string tileName)
        {
            var userId = Clients.Caller.id;
            var player = GameState.Instance.GetPlayer(userId);
            var game = GameState.Instance.FindGame(player);

            if (game!=null)
            {
                //сделать ход и возвратить true если ход сделан
                int[] tileIndex = tileName.Where(char.IsDigit).Select(i => int.Parse(i.ToString())).ToArray();
                bool move = game.Board.Move(tileIndex);
                
                if (move && game.Board.CheckWinner() == game.Board.PlayerTile)
                {
                    Clients.Caller.playerWins();
                    return true;
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
            int[] move = game.Board.Computer.Move();
            Clients.Caller.computerMove(move);

            if (game.Board.CheckWinner() == game.Board.Computer.ComputerTile)
            {
                Clients.Caller.computerWins();
            }
        }
    }
}