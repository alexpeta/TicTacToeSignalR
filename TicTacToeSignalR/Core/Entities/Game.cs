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
        public List<Movement> Moves
        {
            get { return _moves; }
            set { _moves = value; }
        }

        public event EventHandler PlayerHasMoved;
        public void OnPlayerHasMoved()
        {
            var handler = PlayerHasMoved;
            if (handler != null)
            {
                PlayerHasMoved(this, EventArgs.Empty);
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
            Moves = moves;
	    }
        #endregion

        #region NullObject Pattern
        private static readonly Game _nullGame = new Game();

        public static Game Null
        {
            get { return Game._nullGame; }
        }         
        #endregion

        public bool AddMove(Movement move,Guid playerId)
        {
           if(!isValidMove(move,playerId))
           {
               return false;
           }
           else
           {
               return true;
           }
        }

        private bool isValidMove(Movement move, Guid playerId)
        {
            throw new NotImplementedException();
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