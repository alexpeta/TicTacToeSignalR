using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicTacToeSignalR.Core.Mechanics;

namespace TicTacToeSignalR.ViewModel
{
    public class GameViewModel : Game
    {
        private List<Player> _allPlayers = new List<Player>();

        public List<Player> AllPlayers
        {
            get 
            {
                return _allPlayers;
            }
            set { _allPlayers = value; }
        }

        public GameViewModel()
        {
        }
        
    }
}