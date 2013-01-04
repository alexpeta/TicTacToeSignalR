using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TicTacToeSignalR.Tests
{
    [TestClass]
    public class GameTests
    {
        private Game _game = null;
        private Player _p1 = null;
        private Player _p2 = null;
        private Movement _move = null;
         
        [TestInitialize]
        public void Setup()
        {
            _p1 = new Player("Alex", "9C6109F9-5320-4411-9D1C-FA13D1CEC544", string.Empty);
            _p2 = new Player("Bogdan", "3D072B37-0937-43EB-A77F-137B4DD08E03", string.Empty);
            _game = new Game(_p1, _p2);
            _move = new Movement(2, 2, 'x', _p1, false);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _p1 = null;
            _p2 = null;
            _game = null;
            _move = null;
        }

        [TestMethod]
        public void Game_AddMove_ReturnsFalseIfMoveIsNull()
        {
            //Arrange
            Movement move = null;
            string playerId = string.Empty;
            
            //Act
            bool result = _game.AddMove(move, playerId);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Game_AddMove_AddsCorrentMove()
        {
            //Arrage
            int initialMovesListCount = _game.Moves.Count;

            //Act
            bool moveWasAdded = _game.AddMove(_move,_p1.Id);

            //Assert
            Assert.AreEqual<char>(_game.Board[2, 2], 'x');
            Assert.AreNotEqual<int>(initialMovesListCount, _game.Moves.Count);
        }

        [TestMethod]
        public void Game_AddMove_RaisesMoveAddedEventWithTheGivenMovement()
        {
            //Arrange
            NotificationEventArgs<Movement> argsWhenEventIsRaised = null;
            _game.PlayerHasMovedPiece += new EventHandler<NotificationEventArgs<Movement>>((o, e) => { argsWhenEventIsRaised = e; });

            //Act
            bool moveAdded = _game.AddMove(_move, _p1.Id);
            
            //Assert
            Assert.AreEqual<Movement>(argsWhenEventIsRaised.Value,_move);
        }

        [TestMethod]
        public void Game_AddMove_RaisesIsWonEventWhenAddingWinningMoveOnMainDiagonal()
        {
            //Arrange
            NotificationEventArgs<Game> argsWhenGameIsWon = null;
            _game.WonGame += new EventHandler<NotificationEventArgs<Game>>((o, e) => { argsWhenGameIsWon = e; });

            Movement m1 = new Movement(0, 0, 'x', _p1, true);
            Movement m2 = new Movement(1, 1, 'x', _p1, true);
            Movement m3 = new Movement(2, 2, 'x', _p1, true);
            
            Movement m4 = new Movement(0, 1, 'o', _p2, false);
            Movement m5 = new Movement(0, 2, 'o', _p2, false);

            //Act
            bool moveAdded = _game.AddMove(m1, _p1.Id);
            
            moveAdded = _game.AddMove(m4, _p2.Id);
            moveAdded = _game.AddMove(m5, _p2.Id);

            moveAdded = _game.AddMove(m2, _p1.Id);
            moveAdded = _game.AddMove(m3, _p1.Id);
            
            //Assert
            Assert.AreEqual<Game>(argsWhenGameIsWon.Value, _game);
            Assert.IsNotNull(_game.Winner);
        }

        [TestMethod]
        public void Game_AddMove_RaisesIsWonEventWhenAddingWinningMoveOnRow()
        {
            //Arrange
            NotificationEventArgs<Game> argsWhenGameIsWon = null;
            _game.WonGame += new EventHandler<NotificationEventArgs<Game>>((o, e) => { argsWhenGameIsWon = e; });

            Movement m1 = new Movement(0, 0, 'x', _p1, true);
            Movement m2 = new Movement(1, 1, 'o', _p2, false);
            Movement m3 = new Movement(0, 1, 'x', _p1, true);
            Movement m4 = new Movement(2, 2, 'o', _p2, false);
            Movement m5 = new Movement(0, 2, 'x', _p1, true);

            //Act
            bool moveAdded = _game.AddMove(m1, _p1.Id);
            moveAdded = _game.AddMove(m2, _p2.Id);
            moveAdded = _game.AddMove(m3, _p1.Id);
            moveAdded = _game.AddMove(m4, _p2.Id);
            moveAdded = _game.AddMove(m5, _p1.Id);

            //Assert
            Assert.AreEqual<Game>(argsWhenGameIsWon.Value, _game);
            Assert.IsNotNull(_game.Winner);
        }


        [TestMethod]
        public void Game_AddMove_RaisesIsWonEventWhenAddingWinningMoveOnSecondaryDiagonal()
        {
            //Arrange
            NotificationEventArgs<Game> argsWhenGameIsWon = null;
            _game.WonGame += new EventHandler<NotificationEventArgs<Game>>((o, e) => { argsWhenGameIsWon = e; });

            Movement m1 = new Movement(0, 0, 'x', _p1, false);
            Movement m2 = new Movement(0, 2, 'o', _p2, true);
            Movement m3 = new Movement(0, 1, 'x', _p1, false);
            Movement m4 = new Movement(1, 1, 'o', _p2, true);
            Movement m5 = new Movement(1, 0, 'x', _p1, false);
            Movement m6 = new Movement(2, 0, 'o', _p2, true);

            //Act
            bool moveAdded = _game.AddMove(m1, _p1.Id);
            moveAdded = _game.AddMove(m2, _p2.Id);
            moveAdded = _game.AddMove(m3, _p1.Id);
            moveAdded = _game.AddMove(m4, _p2.Id);
            moveAdded = _game.AddMove(m5, _p1.Id);
            moveAdded = _game.AddMove(m6, _p2.Id);

            //Assert
            Assert.AreEqual<Game>(argsWhenGameIsWon.Value, _game);
            Assert.IsNotNull(_game.Winner);
        }

        [TestMethod]
        public void Game_AddMove_RaisesIsWonEventWhenAddingWinningMoveOnColumn()
        {
            //Arrange
            NotificationEventArgs<Game> argsWhenGameIsWon = null;
            _game.WonGame += new EventHandler<NotificationEventArgs<Game>>((o, e) => { argsWhenGameIsWon = e; });

            Movement m1 = new Movement(0, 0, 'x', _p1, false);
            Movement m2 = new Movement(0, 2, 'o', _p2, true);
            Movement m3 = new Movement(1, 0, 'x', _p1, false);
            Movement m4 = new Movement(1, 1, 'o', _p2, true);
            Movement m5 = new Movement(2, 0, 'x', _p1, false);

            //Act
            bool moveAdded = _game.AddMove(m1, _p1.Id);
            moveAdded = _game.AddMove(m2, _p2.Id);
            moveAdded = _game.AddMove(m3, _p1.Id);
            moveAdded = _game.AddMove(m4, _p2.Id);
            moveAdded = _game.AddMove(m5, _p1.Id);

            //Assert
            Assert.AreEqual<Game>(argsWhenGameIsWon.Value, _game);
            Assert.IsNotNull(_game.Winner);
        }

    }
}
