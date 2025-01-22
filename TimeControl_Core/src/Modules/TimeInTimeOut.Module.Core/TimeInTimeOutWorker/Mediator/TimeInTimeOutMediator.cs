

using TimeInTimeOut.Module.Core.TimeInTimeOutWorker.OrchestratorService;

namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.BackgroundService
{
    public class TimeInTimeOutMediator : ITimeInTimeOutMediator
    {
        private readonly IAggregatorServiceTimeInTimeOut aggregatorServiceTimeInTimeOut;
        public TimeInTimeOutMediator(IAggregatorServiceTimeInTimeOut aggregatorServiceTimeInTimeOut)
        =>this.aggregatorServiceTimeInTimeOut=aggregatorServiceTimeInTimeOut;
        
        public Task<bool> UpdateTimeInTimeOutAsync(int userId, bool pingResponseStatus)
        {
            throw new NotImplementedException();
        }
    }
}