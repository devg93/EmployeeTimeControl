

using Shared.Dto;

namespace Shared.Services.ModuleCommunication.Contracts;

    public interface IGetServiceFtomTimeInTimeOutById
    {
         Task<ComingAndGoingDto> GetByIdAsync(int id);
    }
