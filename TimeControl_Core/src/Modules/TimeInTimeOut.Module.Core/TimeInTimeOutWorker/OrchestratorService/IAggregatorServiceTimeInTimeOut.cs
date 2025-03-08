
namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.OrchestratorService
{
    public interface IAggregatorServiceTimeInTimeOut
    {
        Task<bool> UpdateTimeInTimeOut(ComingAndgoingResponseDto entity, bool Status);
    }
}