
using Modules.Break.Module.Core.Astractions.Irepository;
using Modules.Break.Module.Core.Astractions.Irepository.Ibusy;
using Shared.Services.ModuleCommunication.Contracts;
using Shared.Services.Tasks.ShedulerTuplelog;

namespace Break.Module.Core.Abstraction.IServiceProvider
{
    public interface IServicesFactory
    {
        IbreakRepositoryCommand GetBreakRepositoryCommand();
        ITimeHenldeLogService GetTimeHandleLogService();
        IbusyRepositoryCommand GetBusyRepositoryCommand();
        IbusyRepositoryQeury GetBusyRepositoryQeury();

    }

}