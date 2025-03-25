

namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.CommonServices.PersistenceService.Commands;
    public class TimeUpdateCommand:IRequest<bool>
    {
         public int UserId { get; set; }
         public char Param{ get;set;}
    }
