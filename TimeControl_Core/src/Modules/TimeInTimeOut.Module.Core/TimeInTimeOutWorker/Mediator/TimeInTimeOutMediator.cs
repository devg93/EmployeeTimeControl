

using TimeInTimeOut.Module.Core.Dto;
using TimeInTimeOut.Module.Core.TimeInTimeOutWorker.OrchestratorService;

namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.BackgroundService
{
    public class TimeInTimeOutMediator : ITimeInTimeOutMediator
    {
        private readonly IAggregatorServiceTimeInTimeOut aggregatorServiceTimeInTimeOut;
        public TimeInTimeOutMediator(IAggregatorServiceTimeInTimeOut aggregatorServiceTimeInTimeOut)
        => this.aggregatorServiceTimeInTimeOut = aggregatorServiceTimeInTimeOut;

        public async Task<bool> UpdateTimeInTimeOutAsync(int userId, bool pingResponseStatus)
        {

            var Entity = new ComingAndgoingDto
            {
                UserId = userId,
                OnlineTime = pingResponseStatus ? new List<DateTime> { DateTime.Now } : new List<DateTime>(),
                OflineTime = pingResponseStatus ? new List<DateTime>() : new List<DateTime> { DateTime.Now }
            };

            await aggregatorServiceTimeInTimeOut.UpdateTimeInTimeOut(Entity, pingResponseStatus);
            return true;
        }
    }
}