

using MediatR;
using TimeInTimeOut.Module.Core.Abstractions;
using TimeInTimeOut.Module.Core.Domain.Entity;

namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.DAL.Mediatr.Commands
{
    public class CommandHenlder : IRequestHandler<TimeInWriteCommand, bool>
    {
        private readonly IcomingAndgoingRepositoryCommand comingAndgoingRepositoryCommand;
        public CommandHenlder(IcomingAndgoingRepositoryCommand icomingAndgoingRepositoryCommand)
        => this.comingAndgoingRepositoryCommand = icomingAndgoingRepositoryCommand;
        public async Task<bool> Handle(TimeInWriteCommand request, CancellationToken cancellationToken)
        {
            var timeIn = new ComingAndgoing
            {
                OnlineTime = new List<DateTime> { request.OnlineTime },
               

            };
            return await comingAndgoingRepositoryCommand.Add(timeIn);
        }
    }
}