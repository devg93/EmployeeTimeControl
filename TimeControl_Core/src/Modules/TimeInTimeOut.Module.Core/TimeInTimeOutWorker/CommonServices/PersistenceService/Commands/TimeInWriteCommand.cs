

namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.CommonServices.PersistenceService.Commands;
    public class TimeInWriteCommand:IRequest<bool>
    {
        public int Id { get; set; }
        public DateTime OnlineTime { get; set; }
      
    }
