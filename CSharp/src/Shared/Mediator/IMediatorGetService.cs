

using Shared.Services.Tasks.PingCheker;
using Shared.Services.Tasks.ShedulerTuplelog;

namespace Shared.Mediator
{
    public interface IMediatorGetService
    {
       ITimeHenldeLogService TimeHandleLogService { get; }   
       IPingSender pingSender { get; }
       IGetServiceBreake getServiceBreake {get;}
       IGetServiceTimeInTimeOut getServiceTimeInTimeOut {get;}

        
    }
}