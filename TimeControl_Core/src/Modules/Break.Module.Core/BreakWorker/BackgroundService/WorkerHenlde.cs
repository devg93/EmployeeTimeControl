
//************************************ Service WorkerHenlde ******************************************//
// The WorkerHenlde class coordinates operations for managing break times and handling IP-related tasks.

namespace Modules.Break.Module.Core.BreakWorker.Command;

public class WorkerHenlde : IWorkerHenlde
{

    private readonly ILogger<WorkerHenlde> logger;
    private readonly IPingSender pingSender;

    private readonly IOrchestratorService brakeTimeService;
    public WorkerHenlde(ILogger<WorkerHenlde> logger,IPingSender pingIpChecker, IOrchestratorService brakeTimeService)
    => ( this.logger, this.pingSender, this.brakeTimeService) =(logger, pingIpChecker, brakeTimeService);

    public async Task AsyncMethodBreake()
    {

        {
            try
            {
                // get user  -->>>>   from db

                // var PingResponseStatus = await pingSender.PingIp("192.168.100.3");

                var PingResponseStatus = await pingSender.PingIp("192.168.1.94");

                var workSchedule = new BrakeTimeDtoReqvest
                {
                    UserId = 1,
                    StartTime = PingResponseStatus ? new List<DateTime>() : new List<DateTime> { DateTime.Now },
                    EndTime = PingResponseStatus ? new List<DateTime> { DateTime.Now } : new List<DateTime>()
                };

                 await brakeTimeService.AddOrUpdateBrakeTime(workSchedule, PingResponseStatus);


            }

            catch (Exception ex)
            {

                logger.LogError(ex, "Error in WorkerServiceHenlde");

            }
        }
    }
}


public interface IWorkerHenlde
{
    public Task AsyncMethodBreake();
};