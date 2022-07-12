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
    public class AddSupportingPositionCommand : IRequest<CommandResult<SupportingPositionDto>>
    {
        public AddSupportingPositionCommand(CreateSupportingPositionDto createSupportingPositionDto)
        {
            CreateSupportingPositionDto = createSupportingPositionDto;
        }

        public CreateSupportingPositionDto CreateSupportingPositionDto { get; }
    }

    public class AddSupportingPositionCommandHandler : IRequestHandler<AddSupportingPositionCommand, CommandResult<SupportingPositionDto>>
    {
        private readonly IMapper _mapper;
        private readonly ISportRepository _sportRepository;

        public AddSupportingPositionCommandHandler(IMapper mapper, ISportRepository sportRepository)
        {
            _mapper = mapper;
            _sportRepository = sportRepository;
        }

        public Task<CommandResult<SupportingPositionDto>> Handle(AddSupportingPositionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var supportingPosition = _sportRepository.AddSupportingPosition(request.CreateSupportingPositionDto.LeagueId, request.CreateSupportingPositionDto.Name);

                return Task.FromResult(new CommandResult<SupportingPositionDto>(_mapper.Map<SupportingPositionDto>(supportingPosition)));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new CommandResult<SupportingPositionDto>(ex.Message));
            }
        }
    }
}