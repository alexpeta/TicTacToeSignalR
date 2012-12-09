using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToeSignalR
{
    public class Player
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
        public Player(string nick, Guid id)
        {
            Nick = nick;
            Id = id;
        }
    }
}