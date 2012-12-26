using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TicTacToeSignalR.Tests
{
    [TestClass]
    public class InvitationTests
    {
        [TestMethod]
        public void Invitation_DeepCopyConstructor_ReturnsSameObjectDiffrentReference()
        {
            //Arrange
            Guid id = Guid.Parse("A499F757-C538-4559-A156-5D658BDEC0F3");
            Player p1 = null;
            Player p2 = null;
            DateTime dateTime = DateTime.MinValue;
            Invitation first = new Invitation(id, p1, p2, dateTime);

            //Act
            Invitation other = new Invitation(first);

            //Arrage
            Assert.AreEqual<Guid>(first.InviteId, other.InviteId);
            Assert.AreEqual<DateTime>(first.SentDate, other.SentDate);
            Assert.AreEqual(first.From, other.From);
            Assert.AreEqual(first.To, other.To);

            Assert.AreNotSame(first, other);
        }

        [TestMethod]
        public void Invitation_IsValidInvitation_10MinutesLaterInvitationReturnsTrue()
        {
            //Act
            Invitation first = new Invitation();
            Invitation second = new Invitation();
            Player p1 = new Player("Alex", "9C6109F9-5320-4411-9D1C-FA13D1CEC544",string.Empty);
            Player p2 = new Player("Bogdan", "3D072B37-0937-43EB-A77F-137B4DD08E03", string.Empty);

            //Arrange
            first.From = p1;
            first.To = p2;
            first.SentDate = new DateTime(2012, 12, 9, 10, 0, 0);

            second.From = p1;
            second.To = p2;
            second.SentDate = new DateTime(2012, 12, 9, 10, 0, 0) + TimeSpan.FromMinutes(10);

            //Assert
            Assert.IsTrue(second.IsValidInvitation(first));
        }


        [TestMethod]
        public void Invitation_Equals_CheckIEquitableImplemenetationIgnoresReferenceEquals()
        {
            //Act
            Invitation first = new Invitation();
            Invitation second = new Invitation();
            Player p1 = new Player("Alex", "9C6109F9-5320-4411-9D1C-FA13D1CEC544", string.Empty);
            Player p2 = new Player("Alex", "9C6109F9-5320-4411-9D1C-FA13D1CEC544", string.Empty);

            //Arrange
            first.From = p1;
            first.To = p2;
            first.SentDate = new DateTime(2012, 12, 9, 10, 0, 0);

            second.From = p1;
            second.To = p2;
            second.SentDate = new DateTime(2012, 12, 9, 10, 0, 0);

            //Assert
            Assert.IsTrue(first.Equals(second));
        }

        //[TestMethod]
        //public void Invitation_Equals_CheckOverridenOperatorEqualsIgnoresReferenceEquals()
        //{
        //    //Act
        //    Invitation first = new Invitation();
        //    Invitation second = new Invitation();
        //    Player p1 = new Player("Alex", "9C6109F9-5320-4411-9D1C-FA13D1CEC544");
        //    Player p2 = new Player("Alex", "9C6109F9-5320-4411-9D1C-FA13D1CEC544");

        //    //Arrange
        //    first.From = p1;
        //    first.To = p2;
        //    first.SentDate = new DateTime(2012, 12, 9, 10, 0, 0);

        //    second.From = p1;
        //    second.To = p2;
        //    second.SentDate = new DateTime(2012, 12, 9, 10, 0, 0);

        //    //Assert
        //    Assert.IsTrue(first == second);
        //}

        [TestMethod]
        public void Ivitation_DefaultConstructor_CreatesObjectWithValidState()
        {
            //Arrange
            //Act
            Invitation inviteDefaultConstructor = new Invitation();
            Guid generatedGuid = Guid.Empty;

            //Assert
            Assert.AreNotEqual<Guid>(Guid.Empty, inviteDefaultConstructor.InviteId);
        }

        [TestMethod]
        public void Invitation_CreateGameFromInvite_CreatesANewValidObject()
        {
            //Arrange
            Guid id = Guid.Parse("A499F757-C538-4559-A156-5D658BDEC0F3");
            Player p1 = new Player("Alex", "9C6109F9-5320-4411-9D1C-FA13D1CEC544", string.Empty);
            Player p2 = new Player("Bogdan", "3D072B37-0937-43EB-A77F-137B4DD08E03", string.Empty);
            DateTime dateTime = new DateTime(2012, 12, 9, 10, 0, 0);
            Invitation invite = new Invitation(id, p1, p2, dateTime);
            Game gameToTest = null;

            //Act
            gameToTest = invite.CreateGameFromInvite();

            //Assert
            Assert.IsNotNull(gameToTest.GameId);
            Assert.AreEqual(p1, gameToTest.Player1);
            Assert.AreEqual(p2, gameToTest.Player2);
        }

        [TestMethod]
        public void Invitation_InviteToMarkup_CreatesInvitationMarkup()
        {
            //Arrange
            Guid id = Guid.Parse("A499F757-C538-4559-A156-5D658BDEC0F3");
            Player p1 = new Player("Alex", "9C6109F9-5320-4411-9D1C-FA13D1CEC544", string.Empty);
            Player p2 = new Player("Bogdan", "3D072B37-0937-43EB-A77F-137B4DD08E03", string.Empty);
            DateTime dateTime = new DateTime(2012, 12, 9, 10, 0, 0);
            Invitation invite = new Invitation(id, p1, p2, dateTime);
                        
            //Act
            string markup = invite.InviteToMarkup();

            //Assert
            Assert.AreNotEqual<string>(string.Empty, markup);
        }


        [TestMethod]
        public void Invitation_InviteToMarkup_CopesWithNullInviteMembers()
        {
            //Arrange
            Guid id = Guid.Parse("A499F757-C538-4559-A156-5D658BDEC0F3");
            Player p1 = null;
            Player p2 = null;
            DateTime dateTime = DateTime.MinValue;
            Invitation invite = new Invitation(id, p1, p2, dateTime);

            //Act
            string markup = invite.InviteToMarkup();

            //Assert
            Assert.AreEqual<string>(string.Empty, markup);
        }

        [TestMethod]
        public void Invitation_InviteToMarkup_RaisesErrorEvent()
        {
            //Arrange
            Guid id = Guid.Parse("A499F757-C538-4559-A156-5D658BDEC0F3");
            Player p1 = null;
            Player p2 = null;
            DateTime dateTime = DateTime.MinValue;
            Invitation invite = new Invitation(id, p1, p2, dateTime);

            string expectedMessage = "Error when creating invite markup!";
            string gottenMessage = string.Empty;
            EventHandler<NotificationEventArgs<string>> ErrorHandlerFunction = new EventHandler<NotificationEventArgs<string>>((o,e) =>
            {
                gottenMessage = e.Message;
            });
            
            //Act
            invite.ErrorOccurred += ErrorHandlerFunction;
            string markup = invite.InviteToMarkup();

            //Assert
            Assert.AreEqual<string>(expectedMessage, gottenMessage);
        }
    }
}
