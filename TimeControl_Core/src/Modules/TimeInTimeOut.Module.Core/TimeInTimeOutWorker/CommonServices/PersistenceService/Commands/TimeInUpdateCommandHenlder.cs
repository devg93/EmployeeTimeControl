
namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.CommonServices.PersistenceService.Commands;


    public class TimeInUpdateCommandHenlder : IRequestHandler<TimeUpdateCommand, bool>
    {
          private readonly IcomingAndgoingRepositoryCommand comingAndgoingRepositoryCommand;
        public TimeInUpdateCommandHenlder(IcomingAndgoingRepositoryCommand icomingAndgoingRepositoryCommand)
        => this.comingAndgoingRepositoryCommand = icomingAndgoingRepositoryCommand;
        public async Task<bool> Handle(TimeUpdateCommand request, CancellationToken cancellationToken)
        {
           await comingAndgoingRepositoryCommand.UpdateTimeAsync(request.UserId,request.Param);
           return true;
        }
    }
