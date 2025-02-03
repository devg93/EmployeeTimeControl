

using MediatR;
using TimeInTimeOut.Module.Core.Abstractions;
using TimeInTimeOut.Module.Core.Dto;

namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.OrchestratorService.Mediatr.Queries
{
    public class ComingAndgoingQeuriesHeders : IRequestHandler<ComingAndgoingQeuries, ComingAndgoingResponseDto>
    {
        private readonly IcomingAndgoingRepositoryQeury comingAndgoingRepositoryQeury;
        public ComingAndgoingQeuriesHeders(IcomingAndgoingRepositoryQeury icomingAndgoingRepositoryQeury)
        =>comingAndgoingRepositoryQeury = icomingAndgoingRepositoryQeury;
        
        public Task<ComingAndgoingResponseDto> Handle(ComingAndgoingQeuries request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}