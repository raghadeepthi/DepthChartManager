using DepthChartManager.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DepthChartManager.Tests.Domain
{
    public class TeamTests
    {
        [Test]
        public void ShouldThrowExceptionIfTeamNameIsInvalid()
        {
            //Arrange
            var league = new League("NFL");
            // Act
            // Assert
            Assert.Throws<Exception>(() => new Team(league, string.Empty));
        }

        [Test]
        public void ShouldPassIfTeamNameIsValid()
        {
            // Arrange
            var league = new League("NFL");
            var team = new Team(league, "Buffalo Bills");

            //Act
            // Assert
            Assert.AreEqual("Buffalo Bills", team.Name);
        }

        [Test]
        public void ShouldThrowExceptionIfTeamPlayerNameIsInvalid()
        {
            //Arrange
            var league = new League("NFL");
            var team = new Team(league, "Buffalo Bills");

            //Act
            //Assert
            Assert.Throws<Exception>(() => new Player(league, team, string.Empty, string.Empty, 0));
        }


        [Test]
        public void ShouldThrowExceptionIfTeamPlayerNameAlreadyExists()
        {
            //Arrange
            var league = new League("NFL");
            var team = new Team(league, "Buffalo Bills");

            //Act
            team.AddPlayer("Alice", "QA", 0);

            //Assert
            Assert.Throws<Exception>(() => team.AddPlayer("Alice", "QA", 0 ));
        }

        [Test]
        public void ShouldReturnCorrectPlayerCountInATeam()
        {
            //Arrange
            var league = new League("NFL");
            var team = new Team(league, "Buffalo Bills");

            //Act
            team.AddPlayer("Alice", "QB", 0);
            team.AddPlayer("Bob", "QB", 1);

            //Assert
            Assert.AreEqual(2, team.Players.Count());
        }

        [Test]
        public void ShouldAddPlayerAtCorrectPositionInATeam()
        {
            //Arrange
            var league = new League("NFL");
            var team = new Team(league, "Buffalo Bills");

            //Act
            team.AddPlayer("Alice", "QB", 0);
            team.AddPlayer("Bob", "QB", 1);

            //Assert
            Assert.AreEqual(2, team.Players.Count());
            var charliePositionDepth = team.Players.FirstOrDefault(c => c.PlayerName == "Bob");
            Assert.AreEqual(1, charliePositionDepth.PositionDepth);
        }

        [Test]
        public void ShouldAddPlayerAtTheEndOfDepthChartForNoPositionDepth()
        {
            //Arrange
            var league = new League("NFL");
            var team = new Team(league, "Buffalo Bills");

            //Act
            team.AddPlayer("Alice", "QB", 1);
            team.AddPlayer("Bob", "QB", 2);
            team.AddPlayer("Charlie", "QB");

            //Assert
            var charliePositionDepth = team.Players.FirstOrDefault(c => c.PlayerName == "Charlie");
            Assert.AreEqual(0, charliePositionDepth.PositionDepth);
        }

        [Test]
        public void ShouldAddPlayerAtTheEndOfDepthChartForNoPositionAndMovePositionForExistingPlayer()
        {
            //Arrange
            var league = new League("NFL");
            var team = new Team(league, "Buffalo Bills");

            //Act
            team.AddPlayer("Test", "QB", 0);
            team.AddPlayer("Alice", "QB", 1);
            team.AddPlayer("Bob", "QB", 2);
            team.AddPlayer("Charlie", "QB");
            var charliePositionDepth = team.Players.FirstOrDefault(c => c.PlayerName == "Charlie");

            //Assert
            Assert.AreEqual(0, charliePositionDepth.PositionDepth);
            var testPositionDepth = team.Players.FirstOrDefault(c => c.PlayerName == "Test");
            Assert.AreEqual(1, testPositionDepth.PositionDepth);
            var alicePositionDepth = team.Players.FirstOrDefault(c => c.PlayerName == "Alice");
            Assert.AreEqual(2, alicePositionDepth.PositionDepth);
        }


        [Test]
        public void ShouldReturnCorrectPlayerCountInATeamAfterRemovingOnePlayer()
        {
            //Arrange

            var league = new League("NFL");
            var team = new Team(league, "Buffalo Bills");

            //Act
            var alice = team.AddPlayer("Alice", "QA", 0);
            team.AddPlayer("Bob", "QA", 1);
            var removedPlayer = team.RemovePlayer(alice.PlayerName, alice.PositionName);

            //Assert
            Assert.AreEqual("Alice", removedPlayer.PlayerName);
            Assert.AreEqual(1, team.Players.Count());
        }

        [Test]
        public void ShouldReturnEmptyListIfPlayerIsNotListedAtThatPosition()
        {
           //Arrange
           var league = new League("NFL");
           var team = new Team(league, "Buffalo Bills");
           var alice = team.AddPlayer("Alice", "QA", 0);

           //Act
           team.AddPlayer("Bob", "QA", 1);
           var removePlayer = team.RemovePlayer(alice.PlayerName, "LWR");

           //Assert
           Assert.AreEqual(null, removePlayer);
        }


        [Test]
        public void ShouldReturnAllBackupsForPlayerAndPosition()
        {
           //Arrange
           var league = new League("NFL");
           var team = new Team(league, "Buffalo Bills");

           //Act
           var alice = team.AddPlayer("Alice", "QA", 0);
           var bob = team.AddPlayer("Bob", "QA", 1);
           var charlie = team.AddPlayer("Charlie", "QA", 2);
        
           var backUpPlayersForAlice = team.GetBackupPlayerPositions(alice.Id, alice.PositionName);

           //Assert
           Assert.AreNotEqual(null, backUpPlayersForAlice);
          
           Assert.AreEqual("Bob", backUpPlayersForAlice.First().PlayerName);
           Assert.AreEqual("Charlie", backUpPlayersForAlice.Last().PlayerName);
          

           var backUpPlayersForBob = team.GetBackupPlayerPositions(bob.Id, bob.PositionName);
           Assert.AreEqual("Charlie", backUpPlayersForBob.First().PlayerName);


        }

        [Test]
        public void ShouldReturnEmptyListIfNoBackupsForPlayerAndPosition()
        {
            //Arrange
            var league = new League("NFL");
            var team = new Team(league, "Buffalo Bills");

            //Act
            var alice = team.AddPlayer("Alice", "QB", 0);
            var bob = team.AddPlayer("Bob", "LWR", 1);
            var charlie = team.AddPlayer("Charlie", "RWR", 2);

            var backUpPlayersForAlice = team.GetBackupPlayerPositions(alice.Id, alice.PositionName);

            //Assert
            Assert.AreEqual(0, backUpPlayersForAlice.Count());
        }

        [Test]
        public void ShouldReturnEmptyBackUpListIfPlayerIsNotListedAtThatPosition()
        {
            //Arrange
            var league = new League("NFL");
            var team = new Team(league, "Buffalo Bills");
            var alice = team.AddPlayer("Alice", "QB", 0);

            //Act
            team.AddPlayer("Bob", "QB", 1);
            team.AddPlayer("Charlie", "RWR", 2);
            var backUpPlayersForAlice = team.GetBackupPlayerPositions(alice.Id, alice.PositionName);

            //Assert
            Assert.AreEqual("Bob", backUpPlayersForAlice.First().PlayerName);
            var removePlayer = team.RemovePlayer("Bob", "QB");
            var backUpPlayersForAliceAfterRemovingPlayer = team.GetBackupPlayerPositions(alice.Id, alice.PositionName);
            Assert.AreEqual(0, backUpPlayersForAliceAfterRemovingPlayer.Count());
        }

        [Test]
        public void ShouldReturnFullDepth()
        {
           //Arrange
           var league = new League("NFL");
           var team = new Team(league, "Buffalo Bills");
           var alice = team.AddPlayer("Alice", "QB", 0);
           var bob = team.AddPlayer("Bob", "QB", 1);
           var charlie = team.AddPlayer("Charlie", "QB", 2);
           var Mike = team.AddPlayer("Mike", "LWR", 0);
           team.AddPlayer("Jeanlon", "LWR", 1);
           team.AddPlayer("Smith", "RWR", 0);
           foreach (var supportingPositionName in new List<string> { "LWR", "RWR", "LT", "LG", "C", "RG", "RT", "TE", "QB", "RB" })
           {
              league.AddSupportingPosition(supportingPositionName);
           }


           //Act
           var fullDepth = team.GetFullDepth();
           var rwrPlayers = fullDepth.Where(c => c.PositionName == "RWR").ToList();

           //Assert
           Assert.AreEqual(1, rwrPlayers[0].Players.Count());

           var qbPlayers = fullDepth.Where(c => c.PositionName == "QB").ToList();
           Assert.AreEqual(3, qbPlayers[0].Players.Count());

      }


        [Test]
        public void ShouldReturnBackUps()
        {
            //Arrange
            var league = new League("NFL");
            var team = new Team(league, "Buffalo Bills");
            var alice = team.AddPlayer("Alice", "QA", 0);

            //Act
            team.AddPlayer("Bob", "QA", 1);
            var removePlayer = team.RemovePlayer(alice.PlayerName, "LWR");
            //Assert
            Assert.AreEqual(null, removePlayer);
        }

        [Test]
        public void ShouldReturnCorrectPlayerPositionInATeam()
        {
            //Arrange
            var league = new League("NFL");
            var qbPosition = league.AddSupportingPosition("QB");
            league.AddSupportingPosition("WR");
            league.AddSupportingPosition("KR");
            var team = new Team(league, "Buffalo Bills");
            var alice = team.AddPlayer("Alice", "QA", 0);
            var bob = team.AddPlayer("Bob", "LWR", 0);

            //Act
            var playerPosition = team.UpdatePlayerPosition(alice.Id, qbPosition.Id, 0);

            //Assert
            Assert.AreEqual(0, playerPosition.SupportingPositionRanking);
        }

        [Test]
        public void ShouldReturnCorrectPlayerPositionsInATeam()
        {
            //Arrange
            var league = new League("NFL");
            var qbPosition = league.AddSupportingPosition("QB");
            league.AddSupportingPosition("WR");
            league.AddSupportingPosition("KR");

            var team = new Team(league, "Buffalo Bills");
            var alice = team.AddPlayer("Alice", "QA", 0 );
            var bob = team.AddPlayer("Bob", "QA", 1);
            var charlie = team.AddPlayer("Charlie", "QA", 2);

            //Act
            team.UpdatePlayerPosition(alice.Id, qbPosition.Id, 0);
            team.UpdatePlayerPosition(bob.Id, qbPosition.Id, 0);
            team.UpdatePlayerPosition(charlie.Id, qbPosition.Id, 2);

            //Assert
            Assert.AreEqual(bob.Id, team.PlayerPositions.ElementAtOrDefault(0).Player.Id);
            Assert.AreEqual(alice.Id, team.PlayerPositions.ElementAtOrDefault(1).Player.Id);
            Assert.AreEqual(charlie.Id, team.PlayerPositions.ElementAtOrDefault(2).Player.Id);
        }

        [Test]
        public void ShouldNotReturnRemovedPlayerPositionInATeam()
        {
            //Arrange
            var league = new League("NFL");
            var qbPosition = league.AddSupportingPosition("QB");
            league.AddSupportingPosition("WR");
            league.AddSupportingPosition("KR");

            var team = new Team(league, "Buffalo Bills");
            var alice = team.AddPlayer("Alice", "QA", 0);
            var bob = team.AddPlayer("Bob", "QA", 1);
            var charlie = team.AddPlayer("Charlie", "LWR", 0);

            //Act
            team.UpdatePlayerPosition(alice.Id, qbPosition.Id, 0);
            team.UpdatePlayerPosition(bob.Id, qbPosition.Id, 0);
            team.UpdatePlayerPosition(charlie.Id, qbPosition.Id, 2);

            //Assert
            Assert.AreEqual(bob.Id, team.PlayerPositions.ElementAtOrDefault(0).Player.Id);
            Assert.AreEqual(alice.Id, team.PlayerPositions.ElementAtOrDefault(1).Player.Id);
            Assert.AreEqual(charlie.Id, team.PlayerPositions.ElementAtOrDefault(2).Player.Id);

            team.UpdatePlayerPosition(alice.Id, qbPosition.Id, -1);
            Assert.AreEqual(bob.Id, team.PlayerPositions.ElementAtOrDefault(0).Player.Id);
            Assert.AreEqual(charlie.Id, team.PlayerPositions.ElementAtOrDefault(1).Player.Id);

        }



        [Test]
        public void ShouldReturnCorrectPlayerBackupPlayerPositionsInATeam()
        {
          var league = new League("NFL");
          var qbPosition = league.AddSupportingPosition("QB");
          league.AddSupportingPosition("WR");
          league.AddSupportingPosition("KR");

          var team = new Team(league, "Buffalo Bills");
          var alice = team.AddPlayer("Alice", "QB", 1);
          var bob = team.AddPlayer("Bob", "QB");
          var charlie = team.AddPlayer("Charlie", "QB", 2);

          team.UpdatePlayerPosition(alice.Id, qbPosition.Id, 0);
          team.UpdatePlayerPosition(bob.Id, qbPosition.Id, 0);
          team.UpdatePlayerPosition(charlie.Id, qbPosition.Id, 2);

          var backupPlayerPositions = team.GetBackupPlayerPositions(bob.Id, "QB");

          Assert.AreEqual(alice.Id, backupPlayerPositions.ElementAtOrDefault(0).Id);
          Assert.AreEqual(charlie.Id, backupPlayerPositions.ElementAtOrDefault(1).Id);
        }
  }
}
