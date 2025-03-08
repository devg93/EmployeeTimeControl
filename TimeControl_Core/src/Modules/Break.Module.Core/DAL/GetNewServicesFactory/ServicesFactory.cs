
namespace Break.Module.Core.DAL.GetNewServicesFactory
{
  
    public class ServicesFactory : IServicesFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public ServicesFactory(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;
        
        public IbreakRepositoryCommand GetBreakRepositoryCommand()
        => _serviceProvider.GetRequiredService<IbreakRepositoryCommand>();

        public ITimeHenldeLogService GetTimeHandleLogService()
        => _serviceProvider.GetRequiredService<ITimeHenldeLogService>();

        public IbusyRepositoryCommand GetBusyRepositoryCommand()
        => _serviceProvider.GetRequiredService<IbusyRepositoryCommand>();

        public IbusyRepositoryQeury GetBusyRepositoryQeury()
        => _serviceProvider.GetRequiredService<IbusyRepositoryQeury>();
    }
    
    /* generic pattern
    public class ServicesFactory : IServicesFactory
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ServicesFactory(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        private T GetService<T>() where T : notnull
        {
            using var scope = _scopeFactory.CreateScope();
            return scope.ServiceProvider.GetRequiredService<T>();
        }

        public IbreakRepositoryCommand GetBreakRepositoryCommand() => GetService<IbreakRepositoryCommand>();
        public ITimeHenldeLogService GetTimeHandleLogService() => GetService<ITimeHenldeLogService>();
        public IbreakRepositoryQeury GetBreakRepositoryQeury() => GetService<IbreakRepositoryQeury>();
        public ISendServiceToBreakModule GetSendServiceToTimeInTimeOutModule() => GetService<ISendServiceToBreakModule>();
        public IbusyRepositoryCommand GetBusyRepositoryCommand() => GetService<IbusyRepositoryCommand>();
        public IbusyRepositoryQeury GetBusyRepositoryQeury() => GetService<IbusyRepositoryQeury>();

    }
*/

}