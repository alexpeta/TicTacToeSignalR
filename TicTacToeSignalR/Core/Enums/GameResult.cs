using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToeSignalR
{
    internal sealed class GameResult
    {
        public static string None = "None";
        
        public static string PlayerWon = "PlayerWon";
        public static string Draw = "Draw";
        public static string AIWon = "AIWon";

    }
}