
//************************************ Service WorkerHenlde ******************************************//
// The WorkerHenlde class coordinates operations for managing break times and handling IP-related tasks.

namespace Modules.Break.Module.Core.BreakWorker.Command;

public class WorkerHenlde : IWorkerHenlde
{
    
    private readonly ILogger<WorkerHenlde> logger;
    private readonly IBreakTimeUpdateMediator breakeTimeMediator;
    private readonly IPingSender pingSender;
    public WorkerHenlde(ILogger<WorkerHenlde> logger, IBreakTimeUpdateMediator breakeTimeMediator,IPingSender pingIpChecker)
    => (this.breakeTimeMediator, this.logger, this.pingSender) = (breakeTimeMediator, logger, pingIpChecker);

    public async Task AsyncMethodBreake()
    {
      
        {
            try
            {
                // get user  -->>>>   from db

               // var PingResponseStatus = await pingSender.PingIp("192.168.100.3");
              
                  var PingResponseStatus = await pingSender.PingIp("192.168.1.94");
                await breakeTimeMediator.UpdateBreakTimeAsync(1, PingResponseStatus);
            }

            catch (Exception ex)
            {

                logger.LogError(ex, "Error in WorkerServiceHenlde");

            }
        }
    }
}
