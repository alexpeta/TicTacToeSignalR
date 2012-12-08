using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Abstract
{
    public interface IPlayerRepository : IBaseRepository<Player>
    {
        Player GetPlayerById(Guid id);
    }
}
