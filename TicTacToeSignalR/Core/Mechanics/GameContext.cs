using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicTacToeSignalR.Models;

namespace TicTacToeSignalR.Core.Mechanics
{
    public class GameContext
    {
        private List<char> _pieces = new List<char> { 'X','O'};
        public static volatile HashSet<Player> Players = new HashSet<Player>();
        public static volatile HashSet<Game> Games = new HashSet<Game>();

     
        //public void AddPlayer(string nickname)
        //{
        //    Player p = new Player();
        //    p.Nick = nickname;
        //    if (this.Context != null)
        //    {
        //        p.PlayerId = Guid.Parse(this.Context.ConnectionId);
        //    }
        //    else
        //    {
        //        p.PlayerId = Guid.Empty;
        //    }
        //    Players.Add(p);
        //}

        //public Guid GetCurrentConnectionId()
        //{
        //    try
        //    {
        //        return Guid.Parse(this.Context.ConnectionId);
        //    }
        //    catch
        //    {
        //        return Guid.Empty;
        //    }
        //}

        //public void GetServiceState()
        //{
        //    Clients.updateMessages = DateTime.Now.ToShortTimeString();
        //}

        //public void UpdateServiceState()
        //{
        //    Clients.updateMessages = DateTime.Now.ToShortTimeString();
        //}

    }
}