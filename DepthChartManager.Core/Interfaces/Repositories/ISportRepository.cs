using DepthChartManager.Core.Dtos.Response;
using DepthChartManager.Domain;
using System;
using System.Collections.Generic;

namespace DepthChartManager.Core.Interfaces.Repositories
{
    public interface ISportRepository
    {
        League AddLeague(string name);

        IEnumerable<League> GetLeagues();

        SupportingPosition AddSupportingPosition(Guid leagueId, string name);

        SupportingPosition GetSupportingPosition(Guid leagueId, string supportingPositionName);

        IEnumerable<SupportingPosition> GetSupportingPositions(Guid leagueId);

        Team AddTeam(Guid leagueId, string teamName);

        IEnumerable<Team> GetTeams(Guid leagueId);

        Player AddPlayer(Guid leagueId, Guid teamId, string name, string positionName, int positionNumber) ;

        Player RemovePlayer(Guid leagueId, Guid teamId, string playerName, string positionName);

        IEnumerable<Player> GetPlayers(Guid leagueId, Guid teamId);

        Player GetPlayer(Guid leagueId, Guid teamId, string playerName);

        IEnumerable<PlayerPosition> GetPlayerPositions(Guid leagueId, Guid teamId);

        PlayerPosition UpdatePlayerPosition(Guid leagueId, Guid teamId, Guid playerId, Guid supportingPositionId, int supportingPositionRanking);

        IEnumerable<Player> GetBackupPlayerPositions(Guid leagueId, Guid teamId, Guid playerId, string positionName);
        IEnumerable<FullDepthPlayer> GetFullDepthPlayers(Guid leagueId, Guid teamId);
  }
}
