



namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.BackgroundService
{
    public class TimeInTimeOutWorkerHendle : ITimeInTimeOutWorkerHendle
    {

        private readonly ILogger<TimeInTimeOutWorkerHendle> logger;

        private readonly IOrchestratorService aggregatorServiceTimeInTimeOut;
        private readonly IPingSender pingSender;
        public TimeInTimeOutWorkerHendle(ILogger<TimeInTimeOutWorkerHendle> logger, IOrchestratorService aggregatorServiceTimeInTimeOut, IPingSender pingIpChecker)
        => (this.aggregatorServiceTimeInTimeOut, this.logger, this.pingSender) = (aggregatorServiceTimeInTimeOut, logger, pingIpChecker);

        public async Task TimeInTimeOutWorkerAsync()
        {

            {
                try
                {
                    // get user  -->>>>   from db

                    var PingResponseStatus = await pingSender.PingIp("192.168.1.204");


                    var Entity = new ComingAndgoingResponseDto
                    {
                        UserId = 1,
                        OnlineTime = PingResponseStatus ? new List<DateTime> { DateTime.Now } : new List<DateTime>(),
                        OflineTime = PingResponseStatus ? new List<DateTime>() : new List<DateTime> { DateTime.Now }
                    };

                    await aggregatorServiceTimeInTimeOut.UpdateTimeInTimeOut(Entity, PingResponseStatus);
                }

                catch (Exception ex)
                {

                    logger.LogError(ex, "Error in WorkerServiceHenlde");

                }
            }
        }

    }
}


public interface ITimeInTimeOutWorkerHendle
{
    public Task TimeInTimeOutWorkerAsync();

}