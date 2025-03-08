

namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.BackgroundService;

    public interface ITimeInTimeOutMediator
    {
         Task<bool> UpdateTimeInTimeOutAsync(int userId, bool pingResponseStatus);
    }
