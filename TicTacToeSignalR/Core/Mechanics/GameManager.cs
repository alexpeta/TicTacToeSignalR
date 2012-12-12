using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TicTacToeSignalR.Core.Mechanics
{
    public class GameManager
    {
        private static ConcurrentDictionary<Guid, Game> _games = new ConcurrentDictionary<Guid, Game>();
        
        public GameManager()
        {
        }

        public Game CreateGame(Invitation invite)
        {
            Game result = null;

            if (invite != null)
            {
                if (invite.From != null && invite.To != null)
                {
                    result = new Game(invite.From, invite.To);
                }
                else
                {
                    throw new ApplicationException("invite players are null!!");
                }


                if (result != null)
                {
                    _games.TryAdd(result.GameId, result);
                }
            }
            else
            {
                throw new ApplicationException("Can't create Game: null invite!");
            }

            return result;
        }
       
        public string GetBoardMarkup(Guid gameId, Guid playerId)
        {
            Game game = null;
            bool gotValue = _games.TryGetValue(gameId, out game);
            if (game != null)
            {
                if (game.Player1 != null && game.Player1.Id == playerId)
                {
                    return this.RenderBoard(game, 'x');
                }
                else if (game.Player2 != null && game.Player2.Id == playerId)
                {
                    return this.RenderBoard(game, 'o');
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


        private string RenderBoard(Game game, char piece)
        {
            if (game == null || game.Board == null) return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(@"<table class=""board"" data-id=""{0}"">",game.GameId));
            for (int i = 0; i < Game.Dimension; i++)
            {
                sb.Append(@"<tr>");
                for (int j = 0; j < Game.Dimension; j++)
                {
                    if (!char.IsWhiteSpace(game.Board[i, j]) && game.Board[i, j] != Char.MinValue)
                    {
                        sb.Append(string.Format(@"<td><a href=""javascript:alert('set value');"" class=""tile {0}"" data-x=""{1}"" data-y=""{2}"" data-allowed=""{3}""></a></td>", "selected" + game.Board[i, j], i, j, false));
                    }
                    else
                    {
                        sb.Append(string.Format(@"<td><a href=""javascript:alert('set value');"" class=""tile {0}"" data-x=""{1}"" data-y=""{2}"" data-allowed=""{3}""></a></td>", piece, i, j, true));
                    }
                }
                sb.Append(@"</tr>");
            }
            sb.Append("</table>");

            return sb.ToString();
        }
    }
}