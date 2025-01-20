using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Modules.Break.Module.Core.Iservices;
using Shared.Mediator;


//************************************ Service WorkerHenlde ******************************************//
// The WorkerHenlde class coordinates operations for managing break times and handling IP-related tasks.

namespace Modules.Break.Module.Core.BreakWorker.Command;

    public class WorkerHenlde
    {

        private readonly ILogger<WorkerHenlde> logger;
        private readonly IBreakTimeUpdateMediator breakeTimeMediator;
        private readonly IMediatorGetService getService;
        public WorkerHenlde(ILogger<WorkerHenlde> logger, IBreakTimeUpdateMediator breakeTimeMediator,
        IMediatorGetService pingIpChecker)
        => (this.breakeTimeMediator, this.logger, this.getService)= (breakeTimeMediator, logger, pingIpChecker);

        public async Task AsyncMethodBreake()
        {
            

            try
            {
                // get user  -->>>>   from db

                var PingResponseStatus = await getService.pingSender.PingIp("");

                await breakeTimeMediator.UpdateBreakTimeAsync(1, PingResponseStatus);
            }

            catch (Exception ex)
            {


                logger.LogError(ex, "Error in WorkerServiceHenlde");

            }
        }

    }
