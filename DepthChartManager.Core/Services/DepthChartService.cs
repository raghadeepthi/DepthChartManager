using DepthChartManager.Core.Dtos.Request;
using DepthChartManager.Core.Dtos.Response;
using DepthChartManager.Core.Interfaces.Services;
using DepthChartManager.Core.Messaging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepthChartManager.Core.Services
{
    public class DepthChartService : IDepthChartService
    {
        private readonly IMediator _mediator;

        public DepthChartService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<LeagueDto> AddLeague(string leagueName)
        {
            var result = await _mediator.Send(new AddLeagueCommand(new CreateLeagueDto
            {
                Name = leagueName
            }));

            return result.Result;
        }

        public async Task<SupportingPositionDto> AddSupportingPosition(Guid leagueId, string supportingPositionName)
        {
            var result = await _mediator.Send(new AddSupportingPositionCommand(new CreateSupportingPositionDto
            {
                LeagueId = leagueId,
                Name = supportingPositionName
            }));

            return result.Result;
        }

        public async Task<TeamDto> AddTeam(Guid leagueId, string teamName)
        {
            var result = await _mediator.Send(new AddTeamCommand(new CreateTeamDto
            {
                LeagueId = leagueId,
                Name = teamName
            }));

            return result.Result;
        }

        public async Task<PlayerDto> AddPlayer(Guid leagueId, Guid teamId, string playerName, string positionName, int postionDepth = 0)
        {
            var result = await _mediator.Send(new AddPlayerCommand(new CreatePlayerDto
            {
                LeagueId = leagueId,
                TeamId = teamId,
                PlayerName = playerName,
                PositionName = positionName,
                PositionDepth = postionDepth
            }));

            return result.Result;
        }


        public async Task<PlayerDto> RemovePlayer(Guid leagueId, Guid teamId, string positionName, string playerName)
        {
          var result = await _mediator.Send(new RemovePlayerCommand(new RemovePlayerDto()
          {
            LeagueId = leagueId,
            TeamId = teamId,
            PositionName = positionName,
            PlayerName = playerName,
          }));

          return result.Result;
        }

        public async Task<PlayerDto> GetPlayer(Guid leagueId, Guid teamId, string playerName)
        {
            var result = await _mediator.Send(new GetPlayerCommand(new GetPlayerDto
            {
                LeagueId = leagueId,
                TeamId = teamId,
                PlayerName = playerName
            }));

            return result.Result;
        }

        public async Task<SupportingPositionDto> GetSupportingPosition(Guid leagueId, string supportingPositionName)
        {
            var result = await _mediator.Send(new GetSupportingPositionCommand(new GetSupportingPositionDto
            {
                LeagueId = leagueId,
                SupportingPositionName = supportingPositionName
            }));

            return result.Result;
        }

        public async Task<IEnumerable<PlayerPositionDto>> GetPlayerPositions(Guid leagueId, Guid teamId)
        {
            var result = await _mediator.Send(new GetPlayerPositionsCommand(new GetPlayerPositionDto
            {
                LeagueId = leagueId,
                TeamId = teamId,
            }));

            return result.Result;
        }

        public async Task<IEnumerable<PlayerDto>> GetBackupPlayerPositions(Guid leagueId, Guid teamId, string playerName, string positionName)
        {
            var result = await _mediator.Send(new GetBackupPlayersCommand(new GetBackupPlayersDto
            {
                LeagueId = leagueId,
                TeamId = teamId,
                PlayerName = playerName,
                PositionName = positionName
            }));

            return result.Result;
        }


        public async Task<IEnumerable<FullDepthChartDto>> GetFullDepthChart(Guid leagueId, Guid teamId)
        {
            var result = await _mediator.Send(new FullDepthCommand(new GetFullDepthPlayersDto
            {
                LeagueId = leagueId,
                TeamId = teamId
            }));

            return result.Result;
        }

        public async Task<PlayerPositionDto> UpdatePlayerPosition(Guid leagueId, Guid teamId, Guid playerId, Guid supportingPositionId, int supportingPositionRanking)
        {
            var result = await _mediator.Send(new UpdatePlayerPositionCommand(new UpdatePlayerPositionDto
            {
                LeagueId = leagueId,
                TeamId = teamId,
                PlayerId = playerId,
                SupportingPositionId = supportingPositionId,
                SupportingPositionRanking = supportingPositionRanking
            }));

            return result.Result;
        }
    }
}
