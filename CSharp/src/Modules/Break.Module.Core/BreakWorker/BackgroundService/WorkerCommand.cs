
using System.Threading;
using System.Threading.Tasks;
using Break.Module.Core.BreakWorker.BackgroundService;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Modules.Break.Module.Core.BreakWorker.Command;

//******************************** Service WorkerCommand **************************************//
// The WorkerCommand class is a background service that continuously executes tasks in a loop
public class WorkerCommand : BackgroundService
{
    // private readonly ILogger<WorkerCommand> logger;
    // private readonly IWorkerHenlde workerHenlde;
    // public WorkerCommand(ILogger<WorkerCommand> logger,IWorkerHenlde workerHenlde)
    // =>( this.logger,this.workerHenlde  )=(logger,workerHenlde);
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
          
            await Task.Delay(1000, stoppingToken).ConfigureAwait(false);
        }
    }
}
