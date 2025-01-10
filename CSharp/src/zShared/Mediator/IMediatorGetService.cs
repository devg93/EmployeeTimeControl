

using zShared.Services;
using zShared.Services.Tasks.PingCheker;
using zShared.Services.Tasks.ShedulerTuplelog;

namespace zShared.Mediator
{
    public interface IMediatorGetService
    {
       ITimeHenldeLogService TimeHandleLogService { get; }   
       IPingSender pingSender { get; }

        
    }
}