

using Shared.Dto;

namespace Shared;

    public interface IGetServiceFtomTimeInTimeOutById
    {
         Task<ComingAndGoingDto> GetByIdAsync(int id);
    }
