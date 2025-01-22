

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.BackgroundService
{
    public class TimeInTimeOutWorkerCommand : Microsoft.Extensions.Hosting.BackgroundService
    {
         private readonly ILogger<TimeInTimeOutWorkerCommand> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public TimeInTimeOutWorkerCommand(ILogger<TimeInTimeOutWorkerCommand> logger, IServiceScopeFactory serviceScopeFactory)
    => (_logger, _serviceScopeFactory) = (logger ?? throw new ArgumentNullException(nameof(logger))
     , serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory)));

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("TimeInTimeOutWorkerCommand started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var workerHandler = scope.ServiceProvider.GetRequiredService<ITimeInTimeOutWorkerHendle>();

                await workerHandler.TimeInTimeOutWorkerAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception in WorkerCommand.");
            }

            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken).ConfigureAwait(false);
        }

        _logger.LogInformation("TimeInTimeOutWorkerCommand stopped.");
    }

    }
}