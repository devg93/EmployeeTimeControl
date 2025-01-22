

using Shared.Dto;

namespace Shared.Services.ModuleCommunication.Contracts;

    public interface ISendDataToTimeInTimeOutModule
    {
         Task<BrakeTimeDto> GetByIdAsync(int id);
    }
