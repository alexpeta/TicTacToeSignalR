using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TicTacToeSignalR.Core.Mechanics
{
    public static class GameManager
    {
        private static ConcurrentDictionary<Guid, Game> _games = new ConcurrentDictionary<Guid, Game>();
        
        static GameManager()
        {
        }

        private static string RenderBoard(Game game, char piece)
        {
            if (game == null || game.Board == null) return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(@"<table class=""board"" data-id=""{0}"">",game.GameId));
            int k = 0;
            for (int i = 0; i < Game.Dimension; i++)
            {
                sb.Append(@"<tr>");
                for (int j = 0; j < Game.Dimension; j++)
                {
                    sb.Append(string.Format(@"<td><a href=""javascript:$.connection.gameHub.client.callMovePiece('{1}','{2}','{0}','cell{4}');"" class=""tile {0}"" data-x=""{1}"" data-y=""{2}"" data-allowed=""{3}"" id=""cell{4}""></a></td>", piece, i, j, true, k++));
                }
                sb.Append(@"</tr>");
            }
            sb.Append("</table>");

            return sb.ToString();
        }

        #region Public Methods
        public static List<Player> GetPlayersListByGameId(Guid gameId)
        {
            List<Player> result = new List<Player>();
            if (gameId != Guid.Empty)
            {
                Game game = GetGameById(gameId);
                if(game != null)
                {
                    result.Add(game.Player1);
                    result.Add(game.Player2);
                }
            }
            return result;
        }
        public static Game CreateGame(Invitation invite)
        {
            Game result = null;
            if (invite != null)
            {
                if (invite.From != null && invite.To != null)
                {
                    result = new Game(invite.From, invite.To);
                    _games.TryAdd(result.GameId, result);
                }
            }
            return result;
        }
        public static string GetBoardMarkup(Guid gameId, string playerId)
        {
            Game game = null;
            bool gotValue = _games.TryGetValue(gameId, out game);
            if (game != null)
            {
                if (game.Player1 != null && game.Player1.Id == playerId)
                {
                    return GameManager.RenderBoard(game, 'x');
                }
                else if (game.Player2 != null && game.Player2.Id == playerId)
                {
                    return GameManager.RenderBoard(game, 'o');
                }
                else
                {
                    return "Error loading board";
                }
            }
            else
            {
                return "Error loading board";
            }
        }
        public static Game GetGameById(Guid gameId)
        {
            Game result = Game.Null;
            if (_games.ContainsKey(gameId))
            {
                bool gotGame = _games.TryGetValue(gameId,out result);
            }
            return result;
        }
        public static void AddGameMove(Guid gameId, Movement move,string playerId)
        {
            Game currentGame = GameManager.GetGameById(gameId);
            if (currentGame != null)
            {
                currentGame.AddMove(move, playerId);
            }
        }

        public static Game GetGameByPlayerId(string playerId)
        {
            Game result = _games.Where(g=> 
            { 
                if( g.Value != null &&
                    ((g.Value.Player1!= null && g.Value.Player1.Id == playerId) || 
                    (g.Value.Player2!= null && g.Value.Player2.Id == playerId))
                    )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).Select(g=>g.Value).FirstOrDefault();

            return result;
        }
        public static Game QuitGame(Guid gameId, string playerWhoQuitsId)
        {
            Game game = null;
            if (gameId != Guid.Empty)
            {
                game = GameManager.GetGameById(gameId);
            }
            else
            {
                //check to see wheather the player is an a game without any gameId provided.
                game = GameManager.GetGameByPlayerId(playerWhoQuitsId);
            }

            if (game != null)
            {
                game.Winner = game.Player1.Id == playerWhoQuitsId ? game.Player2 : game.Player1;
                //TODO: transfer Game entity to a persistent state.
                _games.TryRemove(game.GameId, out game);
            } 
            return game;
        }
        #endregion

    }
}