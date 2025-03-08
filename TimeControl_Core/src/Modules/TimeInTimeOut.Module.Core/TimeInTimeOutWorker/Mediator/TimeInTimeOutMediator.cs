

namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.BackgroundService
{
    public class TimeInTimeOutMediator : ITimeInTimeOutMediator
    {
        private readonly IAggregatorServiceTimeInTimeOut aggregatorServiceTimeInTimeOut;
        public TimeInTimeOutMediator(IAggregatorServiceTimeInTimeOut aggregatorServiceTimeInTimeOut)
        => this.aggregatorServiceTimeInTimeOut = aggregatorServiceTimeInTimeOut;

        public async Task<bool> UpdateTimeInTimeOutAsync(int userId, bool pingResponseStatus)
        {

            var Entity = new ComingAndgoingResponseDto
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