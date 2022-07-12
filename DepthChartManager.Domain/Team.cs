using DepthChartManager.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DepthChartManager.Domain
{
    public class Team
    {
        private List<Player> _players = new List<Player>();
        private List<PlayerPosition> _playerPositions = new List<PlayerPosition>();

        public Team(League league, string name)
        {
            Contract.Requires<Exception>(league != null, Resource.LeagueNameIsInvalid);
            Contract.Requires<Exception>(!string.IsNullOrWhiteSpace(name), Resource.TeamNameIsInvalid);

            Id = Guid.NewGuid();
            League = league;
            Name = name;
        }

        public Guid Id { get; }

        public League League { get; }
        
        public string Name { get; }

        public IEnumerable<Player> Players => _players.AsReadOnly();

        public IEnumerable<PlayerPosition> PlayerPositions
        {
            get
            {
                var supportingPositionRows = _playerPositions.GroupBy(pp => new { pp.SupportingPosition.Id, pp.SupportingPositionRanking });

                foreach (var supportingPositionRow in supportingPositionRows)
                {
                    var supportingPositionRankingGroups = supportingPositionRow.GroupBy(pp => pp.SupportingPositionRanking);

                    foreach (var supportingPositionRankingGroup in supportingPositionRankingGroups)
                    {
                        foreach (var playerPosition in supportingPositionRankingGroup.Where(r => r.SupportingPositionRanking >= 0).Reverse())
                        {
                            yield return playerPosition;
                        }
                    }
                }
            }
        }

        public Player AddPlayer(string playerName, string positionName, int positionDepth = 0)
        {
            Contract.Requires<Exception>(!string.IsNullOrWhiteSpace(playerName), Resource.PlayerNameIsInvalid);
            Contract.Requires<Exception>(!_players.Exists(player => string.Equals(player.PlayerName, playerName, StringComparison.OrdinalIgnoreCase)), Resource.PlayerAlreadyExistsWithinTeam);
            if (positionDepth == 0)
            {
              _players.Select(c =>
              {
                c.PositionDepth += 1;
                return c;
              }).ToList();
            }
            var player = new Player(League, this, playerName, positionName, positionDepth);
            
            _players.Add(player);
            return player;
        }


        public Player RemovePlayer(string playerName, string positionName)
        {
          // Remove player
          var removePlayer = _players.Find(p => p.PlayerName == playerName && p.PositionName == positionName);
          _players.Remove(removePlayer);
          return removePlayer;
        }

        public Player GetPlayer(string name)
        {
            return _players.Find(p => string.Equals(name, p.PlayerName, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Player> GetBackupPlayerPositions(Guid playerId, string positionName)
        {
            var playerPositionDepth = _players.FindIndex(pp => pp.Id == playerId && pp.PositionName == positionName);
            var players = _players.Where(pp => pp.PositionName == positionName && pp.PositionDepth > playerPositionDepth);
            return players;
        }

        public IEnumerable<FullDepthPlayer> GetFullDepth()
        {
            var fullDepthPlayers = new List<FullDepthPlayer>();  
            var supportingPositions = League.SupportingPositions.ToList();
            foreach(var playerPosition in supportingPositions)
            {
                 var players = _players.FindAll(p => string.Equals(playerPosition.Name, p.PositionName, StringComparison.OrdinalIgnoreCase));
                 if(players.Any())
                 {
                    fullDepthPlayers.Add(new FullDepthPlayer(playerPosition.Name, players));
                 }
            }
            return fullDepthPlayers;
        }

        public PlayerPosition UpdatePlayerPosition(Guid playerId, Guid supportingPositionId, int supportingPositionRanking)
        {
            // Remove if the same player exists in this swimlane
            _playerPositions.RemoveAll(pp => pp.Player.Id == playerId && pp.SupportingPosition.Id == supportingPositionId);

            // Update position
            var player = _players.Find(p => p.Id == playerId);
            var supportingPosition = League.SupportingPositions.FirstOrDefault(s => s.Id == supportingPositionId);
            var playerPosition = new PlayerPosition(League, this, player, supportingPosition, supportingPositionRanking);
            _playerPositions.Add(playerPosition);

            return playerPosition;
        }
    }
}
