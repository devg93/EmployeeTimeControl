



namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.BackgroundService
{
    public class TimeInTimeOutWorkerHendle:ITimeInTimeOutWorkerHendle
    {

    private readonly ILogger<TimeInTimeOutWorkerHendle> logger;
    private readonly ITimeInTimeOutMediator breakTimeUpdateMediator;
    private readonly IPingSender pingSender;
    public TimeInTimeOutWorkerHendle(ILogger<TimeInTimeOutWorkerHendle> logger,ITimeInTimeOutMediator breakTimeUpdateMediator,IPingSender pingIpChecker)
    => (this.breakTimeUpdateMediator, this.logger, this.pingSender) = (breakTimeUpdateMediator, logger, pingIpChecker);

    public async Task TimeInTimeOutWorkerAsync()
    {
      
        {
            try
            {
                // get user  -->>>>   from db

                var PingResponseStatus = await pingSender.PingIp("192.168.1.204");
              
               await breakTimeUpdateMediator.UpdateTimeInTimeOutAsync(1, PingResponseStatus);

            }

            catch (Exception ex)
            {

                logger.LogError(ex, "Error in WorkerServiceHenlde");

            }
        }
    }
        
    }
}