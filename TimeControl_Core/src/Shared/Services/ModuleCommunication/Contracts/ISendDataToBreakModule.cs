

using Shared.Dto;

namespace Shared.Services.ModuleCommunication.Contracts;

    public interface ISendDataToBreakModule
    {
         Task<ComingAndGoingDto> GetByIdAsync(int id);
    }
