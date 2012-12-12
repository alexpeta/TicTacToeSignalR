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
        public GameManager GameManager
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
                HubInviteManager.RemoveInvite(inviteId);
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

            Invitation invitation = HubInviteManager.GetInvitationByInvitationId(answer.InviteId);
            InviteStatus status = HubInviteManager.ValidateAnswer(answer);

            switch (status.StatusType)
            {
                case InviteStatusType.Rejected:
                    Clients.Client(invitation.From.Id.ToString()).test(status.Message);
                    break;
                case InviteStatusType.Accepted:
                default:           
                    //debug code. error is thrown here...
                    Game game = null;
                    game = GameManager.CreateGame(invitation);                   


                    if (game != null)
                    {
                        Clients.Client(invitation.From.Id.ToString()).test("xxx");
                        Clients.Client(invitation.To.Id.ToString()).test("xxx");
                    }
                    else
                    {
                        Clients.Client(invitation.From.Id.ToString()).test("ok");
                        Clients.Client(invitation.To.Id.ToString()).test("ok");
                    }


                    //Clients.Client(game.Player1.Id.ToString()).clientRenderBoard(HubGameManager.GetBoardMarkup(game.GameId, game.Player1.Id));
                    //Clients.Client(game.Player2.Id.ToString()).clientRenderBoard(HubGameManager.GetBoardMarkup(game.GameId, game.Player2.Id));
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

        public Mechanics.GameManager HubGameManager { get; set; }
    }
}