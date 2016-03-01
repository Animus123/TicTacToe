using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicTacToe.Models;

namespace TicTacToe
{
    public class GameState
    {
        //игровое состояние - синглтон
        private static readonly Lazy<GameState> _instance = new Lazy<GameState>(
            () => new GameState(GlobalHost.ConnectionManager.GetHubContext<GameHub>()));

        //коллекция игроков
        private readonly ConcurrentDictionary<string, Player> _players =
            new ConcurrentDictionary<string, Player>(StringComparer.OrdinalIgnoreCase);

        //коллекция игр
        private readonly ConcurrentDictionary<string, Game> _games =
            new ConcurrentDictionary<string, Game>(StringComparer.OrdinalIgnoreCase);

        public GameState(IHubContext hubContext)
        {
            Clients = hubContext.Clients;
        }

        public IHubConnectionContext<dynamic> Clients { get; set; }

        public static GameState Instance
        {
            get { return _instance.Value; }
        }

        public Player CreatePlayer()
        {
            Player p = new Player();
            _players[p.Guid] = p;
            return p;
        }

        internal Game CreateGame(Player player,
            Board.Tile playerTile = Board.Tile.Cross)
        {
            Game game = new Game()
            {
                Player = player,
                Board = new Board(playerTile)
            };

            _games[player.Guid] = game;

            return game;
        }

        public Player GetPlayer(string userId)
        {
            return _players.Values.FirstOrDefault(user => user.Guid == userId);
        }

        public Game FindGame(Player player)
        {
            Game game;
            _games.TryGetValue(player.Guid, out game);
            return game;
        }

        public bool RemoveGame(Player player)
        {
            Game game;
            return _games.TryRemove(player.Guid,out game);
         
        }
    }
}