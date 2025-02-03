
using Shared.Dto;
using Shared.Services.ModuleCommunication.Contracts;
using TimeInTimeOut.Module.Core.Abstractions;


namespace TimeInTimeOut.Module.Core.ServicesCommunication
{
    public class SendDataToBreakModule : ISendServiceToBreakModule
    {
        private readonly IcomingAndgoingRepositoryQeury _icomingAndgoingRepository;

        public SendDataToBreakModule(IcomingAndgoingRepositoryQeury icomingAndgoingRepository)
        {
            _icomingAndgoingRepository = icomingAndgoingRepository;
        }public async Task<ResponseChecker<ComingAndGoingDto>> GetByIdAsync(int id)
{
    var entityResponse = await _icomingAndgoingRepository.GetById(id);

    if (entityResponse is null || entityResponse.Data is null)
    {
        return new ResponseChecker<ComingAndGoingDto>
        {
            IsSuccess = false,
            Message = $"Entity with ID {id} not found or data is null.",
            Data = null
        };
    }

    var entity = entityResponse.Data;

    var dto = new ComingAndGoingDto
    {
        Id = entity.Id,
        OnlineTime = entity.OnlineTime?
            .Where(o => o.TimeIn != null)
            .Select(o => new DateTimeDto { TimeIn = o.TimeIn ?? DateTime.MinValue })
            .ToList() ?? new List<DateTimeDto>(),

        OflineTime = entity.OfflineTime?
            .Where(o => o.TimeOut != null)
            .Select(o => new DateTimeDto { TimeOut = o.TimeOut ?? DateTime.MinValue })
            .ToList() ?? new List<DateTimeDto>()
    };

    return new ResponseChecker<ComingAndGoingDto>
    {
        IsSuccess = true,
        Message = "Entity retrieved successfully.",
        Data = dto
    };
}



    }
}
