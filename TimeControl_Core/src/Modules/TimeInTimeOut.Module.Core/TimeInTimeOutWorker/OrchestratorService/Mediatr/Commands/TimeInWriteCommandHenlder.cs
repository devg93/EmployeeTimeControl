

using MediatR;
using TimeInTimeOut.Module.Core.Abstractions;

namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.OrchestratorService.Mediatr.Commands
{
    public class CommandHenlder : IRequestHandler<TimeInWriteCommand, bool>
    {
        private readonly IcomingAndgoingRepositoryCommand comingAndgoingRepositoryCommand;
        public CommandHenlder(IcomingAndgoingRepositoryCommand icomingAndgoingRepositoryCommand)
        => this.comingAndgoingRepositoryCommand = icomingAndgoingRepositoryCommand;
        public Task<bool> Handle(TimeInWriteCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}