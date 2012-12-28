using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToeSignalR
{
    /// <summary>
    /// Game entity that holds the game context : players, moves, board.
    /// </summary>
    public class Game
    {
        public const int Dimension = 3;

        #region Private Members
        private Guid _gameId;
        private Player _player1;
        private Player _player2;
        private Player _winner;
        private char[,] _board;
        private SortedList<int, Movement> _moves;
        private string _result;
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
        public Player Winner
        {
            get { return _winner; }
            set { _winner = value; }
        }
        public char[,] Board
        {
            get { return _board; }
            set { _board = value; }
        }
        public SortedList<int, Movement> Moves
        {
            get { return _moves; }
        }
        public string Result
        {
            get { return _result; }
        }

        public event EventHandler<NotificationEventArgs<Movement>> PlayerHasMovedPiece;
        public event EventHandler<NotificationEventArgs<string>> ErrorOccurred;
        public event EventHandler<NotificationEventArgs<Game>> UpdateSummary;
        public event EventHandler<NotificationEventArgs<Game>> WonGame;
       
        #region Fire
        private void RaisePlayerHasMoved(NotificationEventArgs<Movement> e)
        {
            var handler = PlayerHasMovedPiece;
            if (handler != null)
            {
                PlayerHasMovedPiece(this,e);
            }
        }
        private void RaiseErrorOccurred(NotificationEventArgs<string> e)
        {
            var handler = this.ErrorOccurred;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        private void RaiseUpdateSummary(NotificationEventArgs<Game> e)
        {
            var handler = this.UpdateSummary;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        private void RaiseWonGame(NotificationEventArgs<Game> e)
        {
            var handler = this.WonGame;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion Fire

        #region Constructors
        public Game()
            : this(Guid.Empty, new char[Game.Dimension, Game.Dimension], Player.Null, Player.Null, new SortedList<int,Movement>())
        {
        }
        public Game(Player p1, Player p2)
            : this(Guid.NewGuid(), new char[Game.Dimension, Game.Dimension], p1, p2, new SortedList<int,Movement>())
        {
        }
        public Game (Guid gameId,char[,] board,Player p1, Player p2, SortedList<int,Movement> moves)
	    {
            GameId = gameId;
            Board = board;
            Player1 = p1;
            Player2 = p2;
            Winner = null;
            _moves = moves;
            _result = GameResult.None;
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
            if (move == null)
            {
                return false;
            }

            //if (this.Moves.Count > 4 && this.Winner != null && this.Winner != Player.Null)
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
                    NotificationEventArgs<string> args = new NotificationEventArgs<string>();
                    args.Message = "Impossible. Invalid move!";
                    RaiseErrorOccurred(args);
                    return false;
                }
                else
                {
                    move.Player = from;
                    Board[move.X, move.Y] = move.Piece;
                    Moves.Add(Moves.Count, move);

                    if (this.IsWon())
                    {
                        this.Winner = from;
                        _result = GameResult.Won;

                    }
                    else if(this.IsDraw())
                    {
                        this.Winner = null;
                        _result = GameResult.Draw;
                    }

                    RaisePlayerHasMoved(new NotificationEventArgs<Movement>(replyTo.Id, from.Nick + move.ToString(), move));
                    RaiseWonGame(new NotificationEventArgs<Game>(this));
                    return true;
                }
            }
            //else
            {
            //    return false;
            }
        }
        
        private bool IsWon()
        {
            char x = 'x';
            char o = 'o';
            //its turn-based so you can get a 3rd piece to win the earliest at the 5th turn
            if (Moves.Count<=4)
            {
                return false;
            }

            //rows
            for (int i = 0; i < Game.Dimension; i++)
            {
                if ((Board[i,0] == x && Board[i,1] == x && Board[i,2] == x) || (Board[i, 0] == o && Board[i, 1] == o && Board[i, 2] == o))
                {
                    Moves.Values.ToList().ForEach(m =>
                    {
                        if (m.X == i && (m.Y == 0 || m.Y == 1 || m.Y == 2))
                        {
                            m.IsWinningMove = true;
                        }
                        else
                        {
                            m.IsWinningMove = false;
                        }
                    });
                    return true;
                }
            }

            //columns
            for (int j = 0; j < Game.Dimension; j++)
            {

                if ((Board[0, j] == x && Board[1, j] == x && Board[2, j] == x) || (Board[0, j] == o && Board[1, j] == o && Board[2, j] == o))
                {
                    Moves.Values.ToList().ForEach(m =>
                    {
                        if (m.Y == j && (m.X == 0 || m.X == 1 || m.X == 2))
                        {
                            m.IsWinningMove = true;
                        }
                        else
                        {
                            m.IsWinningMove = false;
                        }
                    });
                    return true;
                }
            }

            //diagonals
            //principal
            if ((Board[0,0] == x && Board[1,1] == x && Board[2, 2] == x) || (Board[0, 0] == o && Board[1,1] == o && Board[2,2] == o))
            {
                Moves.Values.ToList().ForEach(m =>
                {
                    if (m.Y == m.X)
                    {
                        m.IsWinningMove = true;
                    }
                    else
                    {
                        m.IsWinningMove = false;
                    }
                });
                return true;
            }

            //secondaire
            if ((Board[0, 2] == x && Board[1, 1] == x && Board[2,0] == x) || (Board[0,2] == o && Board[1,1] == o && Board[2, 0] == o))
            {
                Moves.Values.ToList().ForEach(m =>
                {
                    if ((m.X==0 && m.Y ==2)||(m.X==1 && m.Y ==1)||(m.X==2 && m.Y ==0))
                    {
                        m.IsWinningMove = true;
                    }
                    else
                    {
                        m.IsWinningMove = false;
                    }
                });
                return true;
            }

            return false;
        }
        private bool IsDraw()
        {
            return Moves.Count == (Game.Dimension * Game.Dimension);
        }
        private bool isValidMove(Movement move, string playerId)
        {
            //if (Moves.Count == 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    Movement lastMove = Moves.Values.LastOrDefault();
            //    if (lastMove == null)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        if (lastMove.Piece == move.Piece)
            //        {
            //            return false;
            //        }
            //        else
            //        {
            //            return true;
            //        }
            //    }
            //}
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