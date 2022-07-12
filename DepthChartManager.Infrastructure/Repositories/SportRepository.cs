using DepthChartManager.Core.Interfaces.Repositories;
using DepthChartManager.Domain;
using DepthChartManager.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DepthChartManager.Infrastructure.Repositories
{
    public class SportRepository : ISportRepository
    {
        private List<League> _leagues = new List<League>();

        public League AddLeague(string name)
        {
            Contract.Requires<Exception>(!string.IsNullOrWhiteSpace(name), Resource.LeagueNameIsInvalid);
            Contract.Requires<Exception>(!_leagues.Exists(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase)), Resource.LeagueAlreadyExists);

            var sport = new League(name);
            _leagues.Add(sport);
            return sport;
        }

        public League GetLeague(Guid leagueId)
        {
            return _leagues.Find(s => s.Id == leagueId);
        }

        public IEnumerable<League> GetLeagues()
        {
            return _leagues.AsReadOnly();
        }

        public SupportingPosition AddSupportingPosition(Guid leagueId, string name)
        {
            return GetLeague(leagueId)?.AddSupportingPosition(name);
        }

        public SupportingPosition GetSupportingPosition(Guid leagueId, string supportingPositionName)
        {
            return GetSupportingPositions(leagueId).FirstOrDefault(s => string.Equals(supportingPositionName, s.Name, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<SupportingPosition> GetSupportingPositions(Guid leagueId)
        {
            return GetLeague(leagueId)?.SupportingPositions;
        }

        public Team AddTeam(Guid leagueId, string teamName)
        {
            return GetLeague(leagueId)?.AddTeam(teamName);
        }

        public IEnumerable<Team> GetTeams(Guid leagueId)
        {
            return GetLeague(leagueId)?.Teams;
        }

        public Player AddPlayer(Guid leagueId, Guid teamId, string playerName, string positionName, int positionNumber)
        {
            return GetLeague(leagueId)?.GetTeam(teamId)?.AddPlayer(playerName, positionName, positionNumber);
        }

        public Player RemovePlayer(Guid leagueId, Guid teamId, string playerName, string positionName)
        {
           return GetLeague(leagueId)?.GetTeam(teamId)?.RemovePlayer(playerName, positionName);
        }

        public IEnumerable<Player> GetPlayers(Guid leagueId, Guid teamId)
        {
            return GetLeague(leagueId)?.GetTeam(teamId)?.Players;
        }

        public Player GetPlayer(Guid leagueId, Guid teamId, string playerName)
        {
            return GetLeague(leagueId)?.GetTeam(teamId)?.GetPlayer(playerName);
        }

        public IEnumerable<PlayerPosition> GetPlayerPositions(Guid leagueId, Guid teamId)
        {
            return GetLeague(leagueId)?.GetTeam(teamId)?.PlayerPositions;
        }

        public PlayerPosition UpdatePlayerPosition(Guid leagueId, Guid teamId, Guid playerId, Guid supportingPositionId, int supportingPositionRanking)
        {
            return GetLeague(leagueId)?.GetTeam(teamId)?.UpdatePlayerPosition(playerId, supportingPositionId, supportingPositionRanking);
        }

        public IEnumerable<Player> GetBackupPlayerPositions(Guid leagueId, Guid teamId, Guid playerId, string positionName)
        {
            return GetLeague(leagueId)?.GetTeam(teamId)?.GetBackupPlayerPositions(playerId, positionName);
        }

        public IEnumerable<FullDepthPlayer> GetFullDepthPlayers(Guid leagueId, Guid teamId)
        {
             return GetLeague(leagueId)?.GetTeam(teamId)?.GetFullDepth();
        }

  }
}
