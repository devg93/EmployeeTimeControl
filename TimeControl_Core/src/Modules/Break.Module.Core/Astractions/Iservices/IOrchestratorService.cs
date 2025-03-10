

namespace Modules.Break.Module.Core.Astractions.Iservices;
    public interface IOrchestratorService
    {
         Task<bool> AddOrUpdateBrakeTime(BrakeTimeDtoReqvest entity, bool Status);

    }
