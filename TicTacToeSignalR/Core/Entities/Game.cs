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
        private char _p1Piece;
        private char _p2Piece;
        private char[,] _board;
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
        public char P1Piece
        {
            get { return _p1Piece; }
            set { _p1Piece = value; }
        }
        public char P2Piece
        {
            get { return _p2Piece; }
            set { _p2Piece = value; }
        }
        public char[,] Board
        {
            get { return _board; }
            set { _board = value; }
        }


        public Game()
        {
            Board = new char[Game.Dimension, Game.Dimension];
            //FillBoard();
        }


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