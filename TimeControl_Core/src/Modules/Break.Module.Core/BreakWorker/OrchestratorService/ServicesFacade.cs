
using Modules.Break.Module.Core.Astractions.Irepository;
using Modules.Break.Module.Core.Astractions.Irepository.Ibusy;
using Shared.Services.ModuleCommunication.Contracts;
using Shared.Services.Tasks.ShedulerTuplelog;

namespace Break.Module.Core.BreakWorker.OrchestratorService
{
    public class ServicesFacade
    {
        public IbreakRepositoryCommand BreakRepositoryCommand { get; }
        public ITimeHenldeLogService TimeHandleLogService { get; }
        public IbreakRepositoryQeury BreakRepositoryQeury { get; }
        public ISendServiceToBreakModule SendServiceToTimeInTimeOutModule { get; }
        public IbusyRepositoryCommand BusyRepositoryCommand { get; }
        public IbusyRepositoryQeury BusyRepositoryQeury { get; }

        public ServicesFacade(
            IbreakRepositoryCommand breakRepositoryCommand,
            ITimeHenldeLogService timeHandleLogService,
            IbreakRepositoryQeury breakRepositoryQeury,
            ISendServiceToBreakModule sendServiceToTimeInTimeOutModule,
            IbusyRepositoryCommand busyRepositoryCommand,
            IbusyRepositoryQeury busyRepositoryQeury)
        {
            BreakRepositoryCommand = breakRepositoryCommand;
            TimeHandleLogService = timeHandleLogService;
            BreakRepositoryQeury = breakRepositoryQeury;
            SendServiceToTimeInTimeOutModule = sendServiceToTimeInTimeOutModule;
            BusyRepositoryCommand = busyRepositoryCommand;
            BusyRepositoryQeury = busyRepositoryQeury;
        }
    }
}