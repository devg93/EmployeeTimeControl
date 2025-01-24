

using Shared.Dto;

namespace Shared.Services.ModuleCommunication.Contracts;

    public interface ISendServiceToTimeInTimeOutModule
    {
         Task<BrakeTimeDto> GetByIdAsync(int id);
    }

