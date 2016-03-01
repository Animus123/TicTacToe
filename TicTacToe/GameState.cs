﻿using Microsoft.AspNet.SignalR;
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
        private static readonly Lazy<GameState> _instance = new Lazy<GameState>(
            () => new GameState(GlobalHost.ConnectionManager.GetHubContext<GameHub>()));

        private readonly ConcurrentDictionary<string, Player> _players =
            new ConcurrentDictionary<string, Player>(StringComparer.OrdinalIgnoreCase);

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

        public Player CreaterPlayer()
        {
            Player p = new Player();
            _players[p.Id] = p;
            return p;
        }

        internal Game CreateGame(Player player)
        {
            Game game = new Game()
            {
                Player = player,
                Board = new Board()
            };

            _games[player.Id] = game;

            return game;
        }

        public Player GetPlayer(string userId)
        {
            return _players.Values.FirstOrDefault(user => user.Id == userId);
        }

        public Game FindGame(Player player)
        {
            Game game;
            _games.TryGetValue(player.Id, out game);
            return game;
        }

        public bool RemoveGame(Player player)
        {
            Game game;
            return _games.TryRemove(player.Id,out game);
         
        }
    }
}