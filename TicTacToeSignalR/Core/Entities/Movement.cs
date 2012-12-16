using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToeSignalR
{
    public class Movement
    {
        private int _x;
        private int _y;
        private char _piece;

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
        public char Piece
        {
            get { return _piece; }
            set { _piece = value; }
        }

        public Movement() : this(0,0,char.MinValue)
        {
        }
        public Movement(int x, int y, char piece)
        {
            X = x;
            Y = y;
            Piece = piece;
        }
    }
}