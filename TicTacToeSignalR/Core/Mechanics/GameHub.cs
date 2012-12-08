using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR.Hubs;
using TicTacToeSignalR.Models;

namespace TicTacToeSignalR.Core.Mechanics
{
    public class GameHub : Hub
    {
        static ConcurrentDictionary<Guid, Player> _players = new ConcurrentDictionary<Guid, Player>();
        static List<string> moves = new List<string>();

      


        public void GetGameMoves()
        {
            Clients.All.updateSummary(moves);
        }

        public void UpdateGameMoves()
        {
            moves.Add("test + " + Context.ConnectionId);
            Clients.All.updateSummary(moves);
        }


        public void PlayerJoined()
        {
            Player johnDoe = new Player();
            johnDoe.Nick = Clients.Caller.name;
            johnDoe.Id = Guid.Parse(Context.ConnectionId);
            _players.TryAdd(johnDoe.Id, johnDoe);
        }

        public void GetConnectedPlayers()
        {
            List<string> allPlayerNames = _players.Values.Select(p => p.Nick).ToList();
            Clients.All.updateSummary(allPlayerNames);
        }

        public override Task OnDisconnected()
        {
            try
            {
                Guid guid = Guid.Parse(this.Context.ConnectionId);
                Player toRemove = null;
                bool hasBeenRemoved =_players.TryRemove(guid,out toRemove);
                if (hasBeenRemoved && toRemove != null)
                {
                    return Clients.All.appendSummary(string.Format("Player {0} has left the game. You win!", toRemove.Nick));
                }
                else
                {
                    return Clients.All.appendSummary(string.Format("Player {0} has left the game. You win!", toRemove.Nick));
                }
            }
            catch
            {
                return Clients.All.appendSummary("Player has left the game. You win!");
            }
        }
    }
}