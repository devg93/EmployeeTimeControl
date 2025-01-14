using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Modules.Break.Module.Core.BreakWorker
{
    public class WorkerServiceCommand : BackgroundService
    {
        private readonly ILogger<WorkerServiceCommand> logger;
        public WorkerServiceCommand(ILogger<WorkerServiceCommand> logger)
        {
            this.logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                await Task.Delay(1000, stoppingToken).ConfigureAwait(false);

            }
        }
    }
}