using System.Threading.Tasks;
using Break.Module.Core.Dto;

namespace Break.Module.Core.Astractions.Iservices
{
    public interface IBrakeTimeService
    {
         Task<bool> addService(BrakeTimeDtoReqvest entity, bool Status);

    }
}