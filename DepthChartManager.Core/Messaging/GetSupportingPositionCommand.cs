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
    public class GetSupportingPositionCommand : IRequest<CommandResult<SupportingPositionDto>>
    {
        public GetSupportingPositionCommand(GetSupportingPositionDto supportingPositionDto)
        {
            SupportingPositionDto = supportingPositionDto;
        }

        public GetSupportingPositionDto SupportingPositionDto { get; }
    }

    public class GetSupportingPositionCommandHandler : IRequestHandler<GetSupportingPositionCommand, CommandResult<SupportingPositionDto>>
    {
        private readonly IMapper _mapper;
        private readonly ISportRepository _sportRepository;

        public GetSupportingPositionCommandHandler(IMapper mapper, ISportRepository sportRepository)
        {
            _mapper = mapper;
            _sportRepository = sportRepository;
        }

        public Task<CommandResult<SupportingPositionDto>> Handle(GetSupportingPositionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var supportingPosition = _sportRepository.GetSupportingPosition(request.SupportingPositionDto.LeagueId, request.SupportingPositionDto.SupportingPositionName);
                return Task.FromResult(new CommandResult<SupportingPositionDto>(_mapper.Map<SupportingPositionDto>(supportingPosition)));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new CommandResult<SupportingPositionDto>(ex.Message));
            }
        }
    }
}