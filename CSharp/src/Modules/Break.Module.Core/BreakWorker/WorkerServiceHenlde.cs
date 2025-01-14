using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Modules.Break.Module.Core.Mediator;
using Shared.Services.Tasks.PingCheker;

namespace Modules.Break.Module.Core.BreakWorker
{
    public class WorkerServiceHenlde
    {
       
        private readonly ILogger<WorkerServiceHenlde> logger;
        private readonly IBreakeTimeMediator breakeTimeMediator;
        private readonly IPingSender pingIpChecker;
        public WorkerServiceHenlde( ILogger<WorkerServiceHenlde> logger ,IBreakeTimeMediator breakeTimeMediator,
        IPingSender pingIpChecker)
        => (this.breakeTimeMediator, this.logger, this.pingIpChecker) 
        = (breakeTimeMediator, logger, pingIpChecker);
          
        
       

         public async Task AsyncMethodBreake()
        {

            try
            { 
                 // get user  -->>>>   from db

                 var PingResponseStatus= await pingIpChecker.PingIp("");

                 await breakeTimeMediator.UpdateAsync(1, PingResponseStatus);
            }

            catch (Exception ex)
            {


                logger.LogError(ex, "Error in WorkerServiceHenlde");

            }
        }

    }
}