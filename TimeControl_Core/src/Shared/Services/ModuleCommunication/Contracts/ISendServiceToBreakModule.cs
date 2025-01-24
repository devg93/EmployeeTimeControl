

using Shared.Dto;

namespace Shared.Services.ModuleCommunication.Contracts;

    public interface ISendServiceToBreakModule
    {
         Task<ComingAndGoingDto> GetByIdAsync(int id);
    }
