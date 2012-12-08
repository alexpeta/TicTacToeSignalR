using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TicTacToeSignalR.Models;

namespace TicTacToeSignalR.ExtensionMethods
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Renders the GameBoard
        /// </summary>
        /// <param name="helper"></param>
        public static MvcHtmlString RenderBoard(this HtmlHelper helper, Game game , char piece)
        {
            if(game == null) return MvcHtmlString.Create(string.Empty);

            if (game.Board == null) return MvcHtmlString.Create(string.Empty);
            
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<table class=""board"">");
            for (int i = 0; i < Game.Dimension; i++)
            {
                sb.Append(@"<tr>");
                for (int j = 0; j < Game.Dimension; j++)
                {
                    if (!char.IsWhiteSpace(game.Board[i, j]) && game.Board[i, j] != Char.MinValue)
                    {
                        sb.Append(string.Format(@"<td><a href=""javascript:void(0);"" class=""tile {0}"" data-x=""{1}"" data-y=""{2}"" data-allowed=""{3}""></a></td>", "selected" + game.Board[i, j], i, j, false));
                    }
                    else
                    {
                        sb.Append(string.Format(@"<td><a href=""javascript:void(0);"" class=""tile {0}"" data-x=""{1}"" data-y=""{2}"" data-allowed=""{3}""></a></td>", piece, i, j, true));
                    }
                }
                sb.Append(@"</tr>");
            }
            sb.Append("</table>");

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}