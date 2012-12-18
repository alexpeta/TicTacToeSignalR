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
        private static ConcurrentDictionary<string, Player> _lobby = new ConcurrentDictionary<string, Player>();
        #endregion

        #region Constructors
        public GameHub()
        {
        }
        #endregion


        #region Public Methods
        public void AutoCloseInvite(Guid inviteId)
        {
            if (inviteId != Guid.Empty)
            {
                Invitation sentInvite = InviteManager.GetInvitationByInvitationId(inviteId);
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
            InviteStatus status = InviteManager.ValidateAnswer(answer);
            Invitation invitation = InviteManager.ExtractInvite(answer.InviteId);

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
                    //create the new game
                    Game game = GameManager.CreateGame(invitation);
                    game.PlayerHasMovedPiece -= OnPlayerHasMovedPiece;
                    game.PlayerHasMovedPiece += OnPlayerHasMovedPiece;

                    if (game != null)
                    {
                        Player p1 = null;
                        Player p2 = null;
                        _lobby.TryRemove(game.Player1.Id, out p1);
                        _lobby.TryRemove(game.Player2.Id, out p2);
                        p1 = null;
                        p2 = null;
                        GetConnectedPlayers();

                        //start the game for the players.
                        List<Player> gamePlayers = GameManager.GetPlayersListByGameId(game.GameId);
                        Clients.Client(game.Player1.Id.ToString()).clientRenderBoard(GameManager.GetBoardMarkup(game.GameId, game.Player1.Id), gamePlayers);
                        Clients.Client(game.Player2.Id.ToString()).clientRenderBoard(GameManager.GetBoardMarkup(game.GameId, game.Player2.Id), gamePlayers);

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
            InviteStatus status = InviteManager.IsValidInvite(invitation);
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
        public void CallMovePiece(Guid gameId, Movement move, string playerId)
        {
            GameManager.AddGameMove(gameId, move, playerId);
        }
        public void GetConnectedPlayers()
        {
            var playersList = _lobby.Values.ToList();
            Clients.All.refreshPlayersList(playersList);
        }

        #region Handle Events
        public void OnPlayerHasMovedPiece(object sender, NotificationEventArgs<Movement> e)
        {
            if (e != null)
            {
                Clients.Client(e.ClientId).movePiece(e.Message, e.Value);
            }
        }
        #endregion Handle Events

        #region Overrides
        public override Task OnDisconnected()
        {
            try
            {
                Player toRemove = null;
                bool hasBeenRemoved = _lobby.TryRemove(this.Context.ConnectionId, out toRemove);
                return Task.Factory.StartNew(() => GetConnectedPlayers());
            }
            catch
            {
                return Task.Factory.StartNew(() => GetConnectedPlayers());
            }
        }
        #endregion Overrides
        #endregion Public Methods


    }
}