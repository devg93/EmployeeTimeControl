
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