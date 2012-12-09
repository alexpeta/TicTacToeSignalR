using System;
using System.Collections.Generic;
using System.Linq;
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

        //public static bool operator ==(Player lhs, Player rhs)
        //{
        //    if (lhs == null || rhs == null) return false;

        //    return lhs.Nick == rhs.Nick &&
        //        lhs.Id == rhs.Id;
        //}
        //public static bool operator !=(Player lhs, Player rhs)
        //{
        //    return !(lhs == rhs);
        //}

        //TODO: override get hash code
    }
}