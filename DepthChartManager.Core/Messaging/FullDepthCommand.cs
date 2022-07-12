using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DepthChartManager.Core.Dtos.Request;
using DepthChartManager.Core.Dtos.Response;
using DepthChartManager.Core.Interfaces.Repositories;
using MediatR;

namespace DepthChartManager.Core.Messaging
{
  public class FullDepthCommand : IRequest<CommandResult<IEnumerable<FullDepthChartDto>>>
  {
    public FullDepthCommand(GetFullDepthPlayersDto getFullDepthPlayersDto)
    {
      GetFullDepthPlayersDto = getFullDepthPlayersDto;
    }

    public GetFullDepthPlayersDto GetFullDepthPlayersDto { get; set; }
  }

  public class FullDepthPlayersCommandHandler : IRequestHandler<FullDepthCommand, CommandResult<IEnumerable<FullDepthChartDto>>>
  {
    private readonly IMapper _mapper;
    private readonly ISportRepository _sportRepository;

    public FullDepthPlayersCommandHandler(IMapper mapper, ISportRepository sportRepository)
    {
      _mapper = mapper;
      _sportRepository = sportRepository;
    }


    public Task<CommandResult<IEnumerable<FullDepthChartDto>>> Handle(FullDepthCommand request, CancellationToken cancellationToken)
    {
      try
      {
        var fullDepthPlayers = _sportRepository.GetFullDepthPlayers(request.GetFullDepthPlayersDto.LeagueId, request.GetFullDepthPlayersDto.TeamId);
        return Task.FromResult(new CommandResult<IEnumerable<FullDepthChartDto>>(_mapper.Map<IEnumerable<FullDepthChartDto>>(fullDepthPlayers)));
      }
      catch (Exception ex)
      {
        return Task.FromResult(new CommandResult<IEnumerable<FullDepthChartDto>>(ex.Message));
      }
    }
  }
}
