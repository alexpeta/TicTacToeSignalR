using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TicTacToeSignalR
{
    public class Player : IEquatable<Player>
    {
        private string _nick;
        private Guid _id;

        public string Nick
        {
            get { return _nick; }
            set { _nick = value; }
        }
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        public Player() : this(string.Empty,Guid.Empty)
        {
        }
        public Player(string nick, string id) : this(nick,Guid.Parse(id))
        {
        }
        public Player(string nick, Guid id)
        {
            Nick = nick;
            Id = id;
        }

        public bool Equals(Player other)
        {
            if (other == null) return false;

            return this.Nick == other.Nick &&
                this.Id == other.Id;
        }

        [Obsolete]
        public string GetPlayerMarkup(string currentNick = "")
        {
            StringBuilder sb = new StringBuilder();

            if (string.IsNullOrEmpty(this.Nick))
            {
                return string.Empty;
            }

            sb.Append(string.Format(@"<li><a href=""javascript:$.connection.gameHub.client.inviteToPlayFromClient('{0}','{1}')"" class=""player"">{2}</a></li>", this.Nick, this.Id, this.Nick));

            return sb.ToString();
        }
    }
}