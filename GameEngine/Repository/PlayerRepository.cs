using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TicTacToeSignalR.Models;
using GameEngine.Abstract;

namespace GameEngine.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        public Player GetPlayerById(Guid id)
        {
            GameDataContext x = new GameDataContext();
            return x.Players.FirstOrDefault();
        }

        public bool AddOrReplace(Player entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Player entity)
        {
            throw new NotImplementedException();
        }
    }
}
