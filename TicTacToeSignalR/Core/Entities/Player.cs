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
        private string _id;
        private string _avatar;

        public string Nick
        {
            get { return _nick; }
            set { _nick = value; }
        }
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; }
        }

        public string AvatarUrl
        {
            get { return @"\Content\avatars\" + Avatar; }
        }

        #region Constructors
        public Player(Player other):this(other.Nick,other.Id,other.Avatar)
        {
        }
        public Player() : this(string.Empty,string.Empty,string.Empty)
        {
        }
        public Player(string nick, string id,string avatar)
        {
            Nick = nick;
            Id = id;
            Avatar = avatar;
        }
        #endregion Constructors

        #region Null Object Pattern
        private static readonly Player _nullPlayer = new Player(string.Empty, string.Empty, string.Empty);
        public static Player Null
        {
            get { return Player._nullPlayer; }
        }         
        #endregion



        public bool Equals(Player other)
        {
            if (other == null) return false;

            return this.Nick == other.Nick &&
                this.Id == other.Id &&
                this.Avatar == other.Avatar;
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