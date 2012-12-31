using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToeSignalR
{
    public class Movement
    {
        #region Private Members
        private Player _player;
        private int _x;
        private int _y;
        private char _piece;
        private bool _isWinningMove;
        #endregion

        #region Public Properties
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
        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }
        public bool IsWinningMove
        {
            get { return _isWinningMove; }
            set { _isWinningMove = value; }
        }
        #endregion

        #region Constructors
        public Movement() : this(0,0,char.MinValue,null,false)
        {
        }
        public Movement(int x, int y, char piece,Player player,bool isWinningMove)
        {
            X = x;
            Y = y;
            Piece = piece;
            Player = player;
            IsWinningMove = isWinningMove;
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            string nick = string.Empty;
            if (Player != null)
            {
                nick = Player.Nick;
            }                
            return string.Format(@"{0} moved piece at board[{1}][{2}] with {3}",nick,X,Y,Piece);
        }
        #endregion Overrides
    }
}