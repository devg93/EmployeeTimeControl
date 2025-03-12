

namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.CommonServices.PersistenceService.Commands;
    public class TimeOutWriCommands:IRequest<bool>
    {
        
        public int Id { get; set; }
        public DateTime OflineTime { get; set; }
    }
