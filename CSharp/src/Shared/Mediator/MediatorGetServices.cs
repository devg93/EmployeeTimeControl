using Shared.Mediator;
using Shared.Services.ModuleCommunication.Contracts;
using Shared.Services.Tasks.PingCheker;
using Shared.Services.Tasks.ShedulerTuplelog;

public class MediatorGetServices : IMediatorGetService
{
    public ITimeHenldeLogService TimeHandleLogService { get; private set; }
    public IPingSender pingSender { get; private set; }
    public IGetServiceFromBreakById getServiceBreake { get; private set; }
    public IGetServiceFtomTimeInTimeOutById getServiceTimeInTimeOut { get; private set; }

    public MediatorGetServices(
        ITimeHenldeLogService timeHandleLogService,
        IPingSender pingSender,
        IGetServiceFromBreakById getServiceBreake,
        IGetServiceFtomTimeInTimeOutById getServiceTimeInTimeOut)
    {
        TimeHandleLogService = timeHandleLogService;
        this.pingSender = pingSender;
        this.getServiceBreake = getServiceBreake;
        this.getServiceTimeInTimeOut = getServiceTimeInTimeOut;
    }
}
