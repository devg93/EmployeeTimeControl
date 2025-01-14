using System.Threading.Tasks;
using Modules.Break.Module.Core.Dto;


namespace Modules.Break.Module.Core.Astractions.Iservices;
    public interface IBrakeTimeService
    {
         Task<bool> addService(BrakeTimeDtoReqvest entity, bool Status);

    }
