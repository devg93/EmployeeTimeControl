using System.Threading.Tasks;
using Modules.Break.Module.Core.Dto;


namespace Modules.Break.Module.Core.Astractions.Iservices;
    public interface IAggregatorServiceBrakeTime
    {
         Task<bool> AddOrUpdateBrakeTime(BrakeTimeDtoReqvest entity, bool Status);

    }
