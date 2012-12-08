using System;
using System.Linq;

using TicTacToeSignalR.Models;

namespace TicTacToeSignalR.Core
{
    public class PlayerRepository : IPlayerRepository
    {
        public Player GetPlayerById(Guid id)
        {
            //using (GameDataContext db = new GameDataContext())
            //{
            //    return db.Players.Where(player => player.PlayerId == id).SingleOrDefault();
            //}
            throw new NotImplementedException();
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
