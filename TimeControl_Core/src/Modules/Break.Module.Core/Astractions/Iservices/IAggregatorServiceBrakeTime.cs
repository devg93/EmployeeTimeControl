

namespace Modules.Break.Module.Core.Astractions.Iservices;
    public interface IAggregatorServiceBrakeTime
    {
         Task<bool> AddOrUpdateBrakeTime(BrakeTimeDtoReqvest entity, bool Status);

    }
