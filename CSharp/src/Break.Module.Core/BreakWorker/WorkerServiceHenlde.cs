using System;
using System.Threading.Tasks;
using Break.Module.Core.Astractions.Iservices;
using Microsoft.Extensions.Logging;
namespace Break.Module.Core.BreakWorker
{
    public class WorkerServiceHenlde
    {
        private readonly IBrakeTimeService BrakeService;
        private readonly ILogger<WorkerServiceHenlde> logger;
        private readonly IPingSender _pingIpChecker;
        public WorkerServiceHenlde( ILogger<WorkerServiceHenlde> logger 
        ,IBrakeTimeService BrakeService)
        => (this.BrakeService, this.logger) = (BrakeService, logger);
          
        
       

         public async Task AsyncMethodBreake()
        {

            try
            { 
                 // get user  -->>>>   from db

                 var PingResponseStatus= await _pingIpChecker.PingIp("");

                await BrakeService.addService(1, PingResponseStatus);
            }

            catch (Exception ex)
            {


                logger.LogError(ex, "Error in WorkerServiceHenlde");

            }
        }

    }
}