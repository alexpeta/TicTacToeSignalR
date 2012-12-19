using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToeSignalR
{
    public class Game
    {
        public const int Dimension = 3;

        #region Private Members
        private Guid _gameId;
        private Player _player1;
        private Player _player2;
        private char[,] _board;
        private List<Movement> _moves;
        #endregion

        public Guid GameId
        {
            get { return _gameId; }
            set { _gameId = value; }
        }
        public Player Player1
        {
            get { return _player1; }
            set { _player1 = value; }
        }
        public Player Player2
        {
            get { return _player2; }
            set { _player2 = value; }
        }
        public char[,] Board
        {
            get { return _board; }
            set { _board = value; }
        }
        //public List<Movement> Moves
        //{
        //    get { return _moves; }
        //    set { _moves = value; }
        //}

        public event EventHandler<NotificationEventArgs<Movement>> PlayerHasMovedPiece;

        private void RaisePlayerHasMoved(NotificationEventArgs<Movement> e)
        {
            var handler = PlayerHasMovedPiece;
            if (handler != null)
            {
                PlayerHasMovedPiece(this,e);
            }
        }


        #region Constructors
        public Game()
            : this(Guid.Empty, new char[Game.Dimension, Game.Dimension], Player.Null, Player.Null, new List<Movement>())
        {
        }
        public Game(Player p1, Player p2)
            : this(Guid.NewGuid(), new char[Game.Dimension, Game.Dimension], p1, p2, new List<Movement>())
        {
        }
        public Game (Guid gameId,char[,] board,Player p1, Player p2, List<Movement> moves)
	    {
            GameId = gameId;
            Board = board;
            Player1 = p1;
            Player2 = p2;
            _moves = moves;
	    }
        #endregion

        #region NullObject Pattern
        private static readonly Game _nullGame = new Game();

        public static Game Null
        {
            get { return Game._nullGame; }
        }         
        #endregion

        public bool AddMove(Movement move,string playerId)
        {
            Player replyTo = null;
            Player from = null;
            if (this.Player1.Id != playerId)
            {
                replyTo = this.Player1;
                from = this.Player2;
            }
            else
            {
                replyTo = this.Player2;
                from = this.Player1;
            }

            if (!isValidMove(move, playerId))
            {
                RaisePlayerHasMoved(new NotificationEventArgs<Movement>(replyTo.Id, "Invalid move", move));
                return false;
            }
            else
            {
                RaisePlayerHasMoved(new NotificationEventArgs<Movement>(replyTo.Id, from.Nick+move.ToString(), move));
                return true;
            }
        }

        private bool isValidMove(Movement move, string playerId)
        {
            return true;
        }

        [Obsolete]
        private void FillBoard()
        {
            char[] pieces = new char[3]{char.MinValue,'x','o'};
            Random r = new Random();

            for (int i = 0; i < Game.Dimension; i++)
            {
                for (int j = 0; j < Game.Dimension; j++)
                {
                    Board[i, j] = pieces[r.Next(0, Game.Dimension)];
                }
            }
        }

    }
}