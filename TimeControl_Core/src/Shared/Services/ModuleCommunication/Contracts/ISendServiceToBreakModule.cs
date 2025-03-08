

using Shared.Dto;
// impliment mediatR
namespace Shared.Services.ModuleCommunication.Contracts;

    public interface ISendServiceToBreakModule
    {
          public Task<ResponseChecker<ComingAndGoingDto>>  GetByIdAsync(int id);
    }
