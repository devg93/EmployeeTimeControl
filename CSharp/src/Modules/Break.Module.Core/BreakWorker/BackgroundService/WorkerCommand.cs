
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
        while (!stoppingToken.IsCancellationRequested)
        {
           using (var scope = serviceScopeFactory.CreateScope())
            {
             
                var workerHenlde =scope.ServiceProvider.GetRequiredService<IWorkerHenlde>();

                try
                {
                    await workerHenlde.AsyncMethodBreake();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error occurred while executing worker.");
                }

                await Task.Delay(1000, stoppingToken).ConfigureAwait(false);
            }

        }
    }
}
