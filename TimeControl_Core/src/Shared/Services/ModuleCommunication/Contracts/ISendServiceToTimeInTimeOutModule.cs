

using Shared.Dto;

namespace Shared.Services.ModuleCommunication.Contracts;

    public interface ISendServiceToTimeInTimeOutModule
    {
         Task<ResponseChecker<BrakeTimeDto>> GetByIdAsync(int id);
    }

