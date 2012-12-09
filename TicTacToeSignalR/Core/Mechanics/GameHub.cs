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
        private static ConcurrentDictionary<Guid, Player> _lobby = new ConcurrentDictionary<Guid, Player>();
        private static ConcurrentDictionary<Guid, Game> _games = new ConcurrentDictionary<Guid, Game>();
        private static ConcurrentBag<Invitation> _invitations = new ConcurrentBag<Invitation>();

        public void SendInviteResponse()
        {
        }

        public void PlayerJoined(Player johnDoe)
        {
            _lobby.TryAdd(johnDoe.Id, johnDoe);
            GetConnectedPlayers();
        }

        public void InviteToPlay(Invitation second)
        {
            second.SentDate = DateTime.Now;
            if (_invitations.Contains(second))
            {
                Invitation first = null;
                _invitations.TryTake(out first);
                if (first != null && second.IsValidInvitation(first))
                {
                    _invitations.Add(second);
                    Clients.Client(second.To.Id.ToString()).showInviteModal(second.From.Nick);
                }
                else
                {
                    _invitations.Add(first);
                    Clients.Client(second.From.Id.ToString()).test("Rejected invitations can be resent 5 minutes apart.");
                }
            }
            else
            {
                _invitations.Add(second);
                Clients.Client(second.To.Id.ToString()).showInviteModal(second.From.Nick);
            }
        }

        public void GetConnectedPlayers()
        {
            List<Player> allPlayers = _lobby.Values.ToList();
            Clients.All.refreshPlayersList(allPlayers);
        }

        #region Overrides
        public override Task OnDisconnected()
        {
            try
            {
                Guid guid = Guid.Parse(this.Context.ConnectionId);
                Player toRemove = null;
                bool hasBeenRemoved = _lobby.TryRemove(guid, out toRemove);
                if (hasBeenRemoved && toRemove != null)
                {
                    return Task.Factory.StartNew(() => GetConnectedPlayers());
                }
                else
                {
                    return Task.Factory.StartNew(() => GetConnectedPlayers());
                }
            }
            catch
            {
                return Task.Factory.StartNew(() => GetConnectedPlayers());
            }
        }
        #endregion
    }
}