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
        #region Static
        private static ConcurrentDictionary<Guid, Player> _lobby = new ConcurrentDictionary<Guid, Player>();
        private static ConcurrentDictionary<Guid, Game> _games = new ConcurrentDictionary<Guid, Game>();
        #endregion

        #region Private properties
        private InviteManager _inviteMananger;
        #endregion

        #region Public Properties
        public InviteManager HubInviteManager
        {
            get { return _inviteMananger; }
            set { _inviteMananger = value; }
        }
        #endregion

        #region Constructors
        public GameHub()
        {
            HubInviteManager = new InviteManager();
        }
        #endregion


        #region Public Methods
        public void SendInviteResponse()
        {
        }

        public void PlayerJoined(Player johnDoe)
        {
            _lobby.TryAdd(johnDoe.Id, johnDoe);
            GetConnectedPlayers();
        }
        public void InviteToPlay(Invitation invitation)
        {
            InviteStatus status = HubInviteManager.IsValidInvite(invitation);
            switch (status.StatusType)
            {
                case InviteStatusType.Invalid:
                    Clients.Client(invitation.From.Id.ToString()).test(status.Message);
                    break;
                case InviteStatusType.Valid:
                default:
                    Clients.Client(invitation.To.Id.ToString()).showInviteModal(invitation.From.Nick);
                    break;
            }
        }

        public void GetConnectedPlayers()
        {
            List<Player> allPlayers = _lobby.Values.ToList();
            Clients.All.refreshPlayersList(allPlayers);
        }
        #endregion

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