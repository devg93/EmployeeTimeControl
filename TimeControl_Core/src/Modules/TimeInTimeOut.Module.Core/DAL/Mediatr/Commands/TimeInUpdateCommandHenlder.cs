

namespace TimeInTimeOut.Module.Core.DAL.Mediatr.Commands
{
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
}