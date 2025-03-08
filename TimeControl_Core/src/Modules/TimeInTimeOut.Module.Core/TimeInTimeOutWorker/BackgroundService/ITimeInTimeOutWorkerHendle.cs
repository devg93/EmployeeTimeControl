
namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.BackgroundService
{
    public interface ITimeInTimeOutWorkerHendle
    {
        public Task TimeInTimeOutWorkerAsync();
        
    }
}