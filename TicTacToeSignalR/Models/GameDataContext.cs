using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TicTacToeSignalR.Models;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace TicTacToeSignalR.Models
{
    public class GameDataContext : DbContext
    {
        //public DbSet<Game> Games { get; set; }
        //public DbSet<Movement> Movements { get; set; }
        //public DbSet<Player> Players { get; set; }
    }
}