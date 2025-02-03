
using MediatR;

namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.OrchestratorService.Mediatr.Commands
{
    public class TimeOutWriteCommandHendler : IRequestHandler<TimeOutWriCommands, bool>
    {
        public Task<bool> Handle(TimeOutWriCommands request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}