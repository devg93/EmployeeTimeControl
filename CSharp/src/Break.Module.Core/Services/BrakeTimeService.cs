using System.Threading.Tasks;
using Break.Module.Core.Astractions.Iservices;
using Break.Module.Core.Dto;
namespace Break.Module.Core.Services
{
    public class BrakeTimeService : IBrakeTimeService
    {
        public async Task<bool> addService(BrakeTimeDtoReqvest entity, bool Status)
        {
            return true;
        }

    }
}