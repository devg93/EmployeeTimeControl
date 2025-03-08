

namespace Break.Module.Core.DAL.GetNewServicesFactory
{
    public class ServicesFacade : IServicesFacade
    {
        // private readonly Lazy<IbreakRepositoryCommand> _breakRepositoryCommand;
        // private readonly Lazy<ITimeHenldeLogService> _timeHandleLogService;
        // private readonly Lazy<IbreakRepositoryQeury> _breakRepositoryQeury;
        // private readonly Lazy<ISendServiceToBreakModule> _sendServiceToTimeInTimeOutModule;
        // private readonly Lazy<IbusyRepositoryCommand> _busyRepositoryCommand;
        // private readonly Lazy<IbusyRepositoryQeury> _busyRepositoryQeury;

        // public ServicesFacade(IServicesFactory servicesFactory)
        // {
        //     _breakRepositoryCommand = new Lazy<IbreakRepositoryCommand>(() => servicesFactory.GetBreakRepositoryCommand());
        //     _timeHandleLogService = new Lazy<ITimeHenldeLogService>(() => servicesFactory.GetTimeHandleLogService());
        //     _breakRepositoryQeury = new Lazy<IbreakRepositoryQeury>(() => servicesFactory.GetBreakRepositoryQeury());
        //     _sendServiceToTimeInTimeOutModule = new Lazy<ISendServiceToBreakModule>(() => servicesFactory.GetSendServiceToTimeInTimeOutModule());
        //     _busyRepositoryCommand = new Lazy<IbusyRepositoryCommand>(() => servicesFactory.GetBusyRepositoryCommand());
        //     _busyRepositoryQeury = new Lazy<IbusyRepositoryQeury>(() => servicesFactory.GetBusyRepositoryQeury());
        // }

        // public IbreakRepositoryCommand BreakRepositoryCommand => _breakRepositoryCommand.Value;
        // public ITimeHenldeLogService TimeHandleLogService => _timeHandleLogService.Value;
        // public IbreakRepositoryQeury BreakRepositoryQeury => _breakRepositoryQeury.Value;
        // public ISendServiceToBreakModule SendServiceToTimeInTimeOutModule => _sendServiceToTimeInTimeOutModule.Value;
        // public IbusyRepositoryCommand BusyRepositoryCommand => _busyRepositoryCommand.Value;
        // public IbusyRepositoryQeury BusyRepositoryQeury => _busyRepositoryQeury.Value;
        public IbreakRepositoryCommand BreakRepositoryCommand => throw new NotImplementedException();

        public ITimeHenldeLogService TimeHandleLogService => throw new NotImplementedException();

        public IbreakRepositoryQeury BreakRepositoryQeury => throw new NotImplementedException();

        public ISendServiceToBreakModule SendServiceToTimeInTimeOutModule => throw new NotImplementedException();

        public IbusyRepositoryCommand BusyRepositoryCommand => throw new NotImplementedException();

        public IbusyRepositoryQeury BusyRepositoryQeury => throw new NotImplementedException();
    }
}
