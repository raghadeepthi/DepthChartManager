using AutoMapper;
using DepthChartManager.Core.Dtos.Request;
using DepthChartManager.Core.Dtos.Response;
using DepthChartManager.Core.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DepthChartManager.Core.Messaging
{
  public class RemovePlayerCommand : IRequest<CommandResult<PlayerDto>>
  {
    public RemovePlayerCommand(RemovePlayerDto removePlayerDto)
    {
      RemovePlayerDto = removePlayerDto;
    }

    public RemovePlayerDto RemovePlayerDto { get; }
  }


  public class RemovePlayerCommandHandler : IRequestHandler<RemovePlayerCommand, CommandResult<PlayerDto>>
  {
    public readonly ISportRepository _sportRepository;
    private readonly IMapper _mapper;

    public RemovePlayerCommandHandler(IMapper mapper, ISportRepository sportRepository)
    {
      _mapper = mapper;
      _sportRepository = sportRepository;
    }

    public Task<CommandResult<PlayerDto>> Handle(RemovePlayerCommand request, CancellationToken cancellationToken)
    {
      try
      {
        var removePlayer = _sportRepository.GetPlayer(request.RemovePlayerDto.LeagueId, request.RemovePlayerDto.TeamId,
          request.RemovePlayerDto.PlayerName);
        var supportingPosition = _sportRepository.GetSupportingPosition(request.RemovePlayerDto.LeagueId, request.RemovePlayerDto.PositionName);
        _sportRepository.RemovePlayer(request.RemovePlayerDto.LeagueId, request.RemovePlayerDto.TeamId, request.RemovePlayerDto.PlayerName, request.RemovePlayerDto.PositionName);
        return Task.FromResult(new CommandResult<PlayerDto>(_mapper.Map<PlayerDto>(removePlayer)));
      }
      catch (Exception ex)
      {
        return Task.FromResult(new CommandResult<PlayerDto>(ex.Message));
      }
    }
  }
}
