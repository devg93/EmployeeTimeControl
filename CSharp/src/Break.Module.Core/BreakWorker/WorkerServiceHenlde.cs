using Microsoft.Extensions.Logging;
namespace Break.Module.Core.BreakWorker
{
    public class WorkerServiceHenlde
    {
        private readonly ILogger<WorkerServiceHenlde> logger;
        public WorkerServiceHenlde( ILogger<WorkerServiceHenlde> logger)
        {
            this.logger = logger;
        }

    }
}