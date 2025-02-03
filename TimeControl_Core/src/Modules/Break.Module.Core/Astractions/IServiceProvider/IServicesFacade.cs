

using Modules.Break.Module.Core.Astractions.Irepository;
using Modules.Break.Module.Core.Astractions.Irepository.Ibusy;
using Shared.Services.ModuleCommunication.Contracts;
using Shared.Services.Tasks.ShedulerTuplelog;

namespace Break.Module.Core.Abstraction.IServiceProvider
{

    public interface IServicesFacade
    {
        IbreakRepositoryCommand BreakRepositoryCommand { get; }
        ITimeHenldeLogService TimeHandleLogService { get; }
        IbreakRepositoryQeury BreakRepositoryQeury { get; }
        ISendServiceToBreakModule SendServiceToTimeInTimeOutModule { get; }
        IbusyRepositoryCommand BusyRepositoryCommand { get; }
        IbusyRepositoryQeury BusyRepositoryQeury { get; }
    }


}