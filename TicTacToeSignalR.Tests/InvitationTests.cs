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
        public void Invitation_IsValidInvitation_10MinutesLaterInvitationReturnsTrue()
        {
            //Act
            Invitation first = new Invitation();
            Invitation second = new Invitation();
            Player p1 = new Player("Alex", "9C6109F9-5320-4411-9D1C-FA13D1CEC544");
            Player p2 = new Player("Bogdan", "3D072B37-0937-43EB-A77F-137B4DD08E03");

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
        public void Invitation_IsValidInvitation_4MinutesLaterInvitationReturnsFalse()
        {
            //Act
            Invitation first = new Invitation();
            Invitation second = new Invitation();
            Player p1 = new Player("Alex", "9C6109F9-5320-4411-9D1C-FA13D1CEC544");
            Player p2 = new Player("Bogdan", "3D072B37-0937-43EB-A77F-137B4DD08E03");

            //Arrange
            first.From = p1;
            first.To = p2;
            first.SentDate = new DateTime(2012, 12, 9, 10, 0, 0);

            second.From = p1;
            second.To = p2;
            second.SentDate = new DateTime(2012, 12, 9, 10, 0, 0) + TimeSpan.FromMinutes(4);

            //Assert
            Assert.IsFalse(second.IsValidInvitation(first));
        }

        [TestMethod]
        public void Invitation_IsValidInvitation_5MinutesLaterInvitationReturnsTrue()
        {
            //Act
            Invitation first = new Invitation();
            Invitation second = new Invitation();
            Player p1 = new Player("Alex", "9C6109F9-5320-4411-9D1C-FA13D1CEC544");
            Player p2 = new Player("Bogdan", "3D072B37-0937-43EB-A77F-137B4DD08E03");

            //Arrange
            first.From = p1;
            first.To = p2;
            first.SentDate = new DateTime(2012, 12, 9, 10, 0, 0);

            second.From = p1;
            second.To = p2;
            second.SentDate = new DateTime(2012, 12, 9, 10, 0, 0) + TimeSpan.FromMinutes(5);

            //Assert
            Assert.IsFalse(second.IsValidInvitation(first));
        }

        [TestMethod]
        public void Invitation_Equals_CheckIEquitableImplemenetationIgnoresReferenceEquals()
        {
            //Act
            Invitation first = new Invitation();
            Invitation second = new Invitation();
            Player p1 = new Player("Alex", "9C6109F9-5320-4411-9D1C-FA13D1CEC544");
            Player p2 = new Player("Alex", "9C6109F9-5320-4411-9D1C-FA13D1CEC544");

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

        [TestMethod]
        public void Invitation_Equals_CheckOverridenOperatorEqualsIgnoresReferenceEquals()
        {
            //Act
            Invitation first = new Invitation();
            Invitation second = new Invitation();
            Player p1 = new Player("Alex", "9C6109F9-5320-4411-9D1C-FA13D1CEC544");
            Player p2 = new Player("Alex", "9C6109F9-5320-4411-9D1C-FA13D1CEC544");

            //Arrange
            first.From = p1;
            first.To = p2;
            first.SentDate = new DateTime(2012, 12, 9, 10, 0, 0);

            second.From = p1;
            second.To = p2;
            second.SentDate = new DateTime(2012, 12, 9, 10, 0, 0);

            //Assert
            Assert.IsTrue(first == second);
        }
    }
}
