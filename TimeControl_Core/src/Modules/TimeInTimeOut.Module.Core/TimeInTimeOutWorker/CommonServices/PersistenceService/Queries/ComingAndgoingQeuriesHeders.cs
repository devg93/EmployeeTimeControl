
namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.CommonServices.PersistenceServiceDb.Queries;

    public class ComingAndgoingQeuriesHeders : IRequestHandler<ComingAndgoingQeuries, ComingAndgoingResponseDto>
    {
        private readonly IcomingAndgoingRepositoryQeury comingAndgoingRepositoryQeury;
        public ComingAndgoingQeuriesHeders(IcomingAndgoingRepositoryQeury icomingAndgoingRepositoryQeury)
        =>comingAndgoingRepositoryQeury = icomingAndgoingRepositoryQeury;
        
        public async Task<ComingAndgoingResponseDto> Handle(ComingAndgoingQeuries request, CancellationToken cancellationToken)
        {
           var responseChecker= await comingAndgoingRepositoryQeury.GetById(request.Id,"OnlineTime");

              if(responseChecker.IsSuccess && responseChecker.Data != null)
              {
                return new ComingAndgoingResponseDto
                {
                    Id = responseChecker.Data.Id,
                    OnlineTime = responseChecker.Data.OnlineTime,
                    OflineTime = responseChecker.Data.OfflineTime,
                    UserId = responseChecker.Data.Id
                };
              }
                return new ComingAndgoingResponseDto{Id = 0, OnlineTime = null, OflineTime = null, UserId = 0};
        }
    }
