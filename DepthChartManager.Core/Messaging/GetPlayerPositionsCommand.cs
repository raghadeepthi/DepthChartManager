using AutoMapper;
using DepthChartManager.Core.Dtos.Request;
using DepthChartManager.Core.Dtos.Response;
using DepthChartManager.Core.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DepthChartManager.Core.Messaging
{
    public class GetPlayerPositionsCommand : IRequest<CommandResult<IEnumerable<PlayerPositionDto>>>
    {
        public GetPlayerPositionsCommand(GetPlayerPositionDto getPlayerPositionDto)
        {
            GetPlayerPositionDto = getPlayerPositionDto;
        }

        public GetPlayerPositionDto GetPlayerPositionDto { get; }
    }

    public class GetPositionsOfPlayersCommandHandler : IRequestHandler<GetPlayerPositionsCommand, CommandResult<IEnumerable<PlayerPositionDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ISportRepository _sportRepository;

        public GetPositionsOfPlayersCommandHandler(IMapper mapper, ISportRepository sportRepository)
        {
            _mapper = mapper;
            _sportRepository = sportRepository;
        }

        public Task<CommandResult<IEnumerable<PlayerPositionDto>>> Handle(GetPlayerPositionsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var playerPositions = _sportRepository.GetPlayerPositions(request.GetPlayerPositionDto.LeagueId, request.GetPlayerPositionDto.TeamId);
                var playerPositionDtos = _mapper.Map<IEnumerable<PlayerPositionDto>>(playerPositions);
                return Task.FromResult(new CommandResult<IEnumerable<PlayerPositionDto>>(playerPositionDtos));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new CommandResult<IEnumerable<PlayerPositionDto>>(ex.Message));
            }
        }
    }
}