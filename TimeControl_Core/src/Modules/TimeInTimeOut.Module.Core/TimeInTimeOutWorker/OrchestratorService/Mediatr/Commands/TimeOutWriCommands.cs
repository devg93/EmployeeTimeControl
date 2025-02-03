
using MediatR;

namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.OrchestratorService.Mediatr.Commands
{
    public class TimeOutWriCommands:IRequest<bool>
    {
        
        public int Id { get; set; }
        public DateTime OflineTime { get; set; }
    }
}