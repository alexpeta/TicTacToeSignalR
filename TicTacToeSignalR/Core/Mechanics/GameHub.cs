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
        #endregion

        #region Private properties
        private InviteManager _inviteMananger;
        private GameManager _gameManager;
        #endregion

        #region Public Properties
        public InviteManager HubInviteManager
        {
            get { return _inviteMananger; }
            set { _inviteMananger = value; }
        }
        public GameManager HubGameManager
        {
            get { return _gameManager; }
            set { _gameManager = value; }
        }
        #endregion

        #region Constructors
        public GameHub()
        {
            HubInviteManager = new InviteManager();
            HubGameManager = new GameManager();
        }
        #endregion


        #region Public Methods
        public void AutoCloseInvite(Guid inviteId)
        {
            if (inviteId != Guid.Empty)
            {
                Invitation sentInvite = HubInviteManager.GetInvitationByInvitationId(inviteId);
                //HubInviteManager.RemoveInvite(inviteId);
                Clients.Client(sentInvite.From.Id.ToString()).test(string.Format("Your invite to {0} has expired.",sentInvite.From.Nick));
            }
        }
        public void SendInviteAnswer(InviteAnswer answer)
        {
            if (answer == null)
            {
                //TODO: make this more robust.
                Clients.Caller.test("Error sending response.");
            }
            InviteStatus status = HubInviteManager.ValidateAnswer(answer);
            Invitation invitation = HubInviteManager.ExtractInvite(answer.InviteId);

            switch (status.StatusType)
            {
                case InviteStatusType.Invalid:
                    Clients.All.test("invalid");
                    break;
                case InviteStatusType.Valid:
                    Clients.All.test("invalid");
                    break;
                case InviteStatusType.Rejected:
                    Clients.Client(invitation.From.Id.ToString()).test(status.Message);
                    break;
                case InviteStatusType.Accepted:
                default:
                    Game game = HubGameManager.CreateGame(invitation);
                    if (game != null)
                    {
                        Clients.Client(game.Player1.Id.ToString()).clientRenderBoard(HubGameManager.GetBoardMarkup(game.GameId, game.Player1.Id));
                        Clients.Client(game.Player2.Id.ToString()).clientRenderBoard(HubGameManager.GetBoardMarkup(game.GameId, game.Player2.Id));
                    }
                    else
                    {
                        Clients.All.test("error game is null");
                    }
                    break;
            }
        }
        public void PlayerJoined(Player johnDoe)
        {
            _lobby.TryAdd(johnDoe.Id, johnDoe);
            GetConnectedPlayers();
        }
        public void InviteToPlay(Invitation invitation)
        {
            InviteStatus status = HubInviteManager.IsValidInvite(invitation);
            //HubInviteManager.AddInvite(invitation);
            switch (status.StatusType)
            {
                case InviteStatusType.Invalid:
                case InviteStatusType.Rejected:
                    Clients.Client(invitation.From.Id.ToString()).test(status.Message);
                    break;
                case InviteStatusType.Valid:
                default:
                    Clients.Client(invitation.To.Id.ToString()).showInviteModal(invitation.InviteToMarkup());
                    break;
            }
        }

        public void GetConnectedPlayers()
        {
            var playersList = _lobby.Values.ToList();
            Clients.All.refreshPlayersList(playersList);
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

                return Task.Factory.StartNew(() => GetConnectedPlayers());
            }
            catch
            {
                return Task.Factory.StartNew(() => GetConnectedPlayers());
            }
        }
        #endregion
    }
}