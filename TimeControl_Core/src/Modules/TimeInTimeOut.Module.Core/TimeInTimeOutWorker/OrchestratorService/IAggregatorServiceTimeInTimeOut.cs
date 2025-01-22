

using TimeInTimeOut.Module.Core.Dto;

namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.OrchestratorService
{
    public interface IAggregatorServiceTimeInTimeOut
    {
        Task<bool> UpdateTimeInTimeOut(ComingAndgoingDto entity, bool Status);
    }
}