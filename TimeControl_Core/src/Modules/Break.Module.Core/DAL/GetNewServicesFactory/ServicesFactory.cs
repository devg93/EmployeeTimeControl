
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

        public IbreakRepositoryCommand GetibreakRepositoryCommand()
        => _serviceProvider.GetRequiredService<IbreakRepositoryCommand>();
        public IbreakRepositoryQeury GetibreakRepositoryQeury()
        => _serviceProvider.GetRequiredService<IbreakRepositoryQeury>();
    }
}