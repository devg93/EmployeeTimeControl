

using Shared.Dto;

namespace Shared.Services.ModuleCommunication.Contracts;

    public interface ISendServiceToBreakModule
    {
          public Task<ResponseComingAndgoin<ComingAndGoingDto>>  GetByIdAsync(int id);
    }
