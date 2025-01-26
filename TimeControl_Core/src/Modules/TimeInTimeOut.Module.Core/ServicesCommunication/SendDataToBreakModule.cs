
using Shared.Dto;
using Shared.Services.ModuleCommunication.Contracts;
using TimeInTimeOut.Module.Core.Abstractions;
using TimeInTimeOut.Module.Core.Domain.Entity;
using TimeInTimeOut.Module.Core.Dto;

namespace TimeInTimeOut.Module.Core.ServicesCommunication
{
    public class SendDataToBreakModule : ISendServiceToBreakModule
    {
        private readonly IcomingAndgoingRepository _icomingAndgoingRepository;

        public SendDataToBreakModule(IcomingAndgoingRepository icomingAndgoingRepository)
        {
            _icomingAndgoingRepository = icomingAndgoingRepository;
        }

        public async Task<ResponseComingAndgoin<ComingAndGoingDto>> GetByIdAsync(int id)
        {
            var entity = await _icomingAndgoingRepository.GetById(id);

            if (entity is null)
            {
                return new ResponseComingAndgoin<ComingAndGoingDto>
                {
                    IsSuccess = false,
                    Message = $"Entity with ID {id} not found.",
                    Data = null
                };
            }

            if (entity.Data is null)
            {
                return new ResponseComingAndgoin<ComingAndGoingDto>
                {
                    IsSuccess = false,
                    Message = $"Entity data with ID {id} is null.",
                    Data = null
                };
            }

            var dto = new ComingAndGoingDto
            {
                Id = entity.Data.Id,
                OnlineTime = entity.Data.OnlineTime?.Select(o => new DateTimeDto
                {
                    TimeIn = o.TimeIn
                }).ToList() ?? new List<DateTimeDto>(),

                OflineTime = entity.Data.OflineTime?.Select(o => new DateTimeDto
                {
                    TimeOut = o.TimeOut
                }).ToList() ?? new List<DateTimeDto>()
            };

            return new ResponseComingAndgoin<ComingAndGoingDto>
            {
                IsSuccess = true,
                Message = "Entity retrieved successfully.",
                Data = dto
            };



        }



    }
}
