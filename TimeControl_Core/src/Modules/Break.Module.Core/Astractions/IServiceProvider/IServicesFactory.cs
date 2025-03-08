

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