

using MediatR;

namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.OrchestratorService.Mediatr.Commands
{
    public class TimeInWriteCommand:IRequest<bool>
    {
        public int Id { get; set; }
        public DateTime OnlineTime { get; set; }
      
    }
}