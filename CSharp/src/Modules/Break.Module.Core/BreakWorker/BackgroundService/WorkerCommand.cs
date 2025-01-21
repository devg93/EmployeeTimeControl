
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
public class WorkerCommand : BackgroundService
{
    private readonly ILogger<WorkerCommand> logger;
    private readonly IWorkerHenlde workerHenlde;
    private readonly IServiceScopeFactory serviceScopeFactory;
    public WorkerCommand(ILogger<WorkerCommand> logger, IWorkerHenlde workerHenlde, IServiceScopeFactory serviceScopeFactory)
    => (this.logger, this.workerHenlde, this.serviceScopeFactory) = (logger, workerHenlde, serviceScopeFactory);

     protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("WorkerCommand started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = serviceScopeFactory.CreateScope();
                var workerHandler = scope.ServiceProvider.GetRequiredService<IWorkerHenlde>();

                await workerHandler.AsyncMethodBreake();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception in WorkerCommand.");
            }

           
            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken).ConfigureAwait(false);
        }

        logger.LogInformation("WorkerCommand stopped.");
    }
}
