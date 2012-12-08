using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicTacToeSignalR.Models;

namespace TicTacToeSignalR.Core.Repository
{
    public class GameRepository : IBaseRepository<Game>
    {
        public bool AddOrReplace(Game entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Game entity)
        {
            throw new NotImplementedException();
        }
    }
}