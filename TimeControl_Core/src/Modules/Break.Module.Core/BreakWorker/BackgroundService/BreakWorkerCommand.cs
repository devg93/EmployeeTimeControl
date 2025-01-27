
using System;
using System.Threading;
using System.Threading.Tasks;
using Break.Module.Core.BreakWorker.BackgroundService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Modules.Break.Module.Core.BreakWorker.Command;

//******************************** Service WorkerCommand **************************************//
// The WorkerCommand class is a background service that continuously executes tasks in a loop
public class BreakWorkerCommand : BackgroundService
{
    private readonly ILogger<BreakWorkerCommand> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public BreakWorkerCommand(ILogger<BreakWorkerCommand> logger, IServiceScopeFactory serviceScopeFactory)
    => (_logger, _serviceScopeFactory) = (logger ?? throw new ArgumentNullException(nameof(logger))
     , serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory)));

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("WorkerCommand started.");

        while (!stoppingToken.IsCancellationRequested)
        {
             try
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var workerHandler = scope.ServiceProvider.GetRequiredService<IWorkerHenlde>();

                await workerHandler.AsyncMethodBreake();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception in WorkerCommand.");
            }

            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken).ConfigureAwait(false);
        }

        _logger.LogInformation("WorkerCommand stopped.");
    }

}
