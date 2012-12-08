using System;
using TicTacToeSignalR.Models;

namespace TicTacToeSignalR.Core
{
    public interface IPlayerRepository : IBaseRepository<Player>
    {
        Player GetPlayerById(Guid id);
    }
}
