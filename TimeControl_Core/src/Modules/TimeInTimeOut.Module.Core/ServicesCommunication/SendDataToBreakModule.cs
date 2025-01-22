
using Shared.Dto;
using Shared.Services.ModuleCommunication.Contracts;
using TimeInTimeOut.Module.Core.Abstractions;

namespace TimeInTimeOut.Module.Core.Services
{
    public class SendDataToBreakModule : ISendDataToBreakModule
    {
        private readonly IcomingAndgoingRepository _icomingAndgoingRepository;

        public SendDataToBreakModule(IcomingAndgoingRepository icomingAndgoingRepository)
        {
            _icomingAndgoingRepository = icomingAndgoingRepository;
        }

        public async Task<ComingAndGoingDto> GetByIdAsync(int id)
        {
           
            var entity = await _icomingAndgoingRepository.GetById(id);

            if (entity == null)
            {
                throw new Exception($"Entity with ID {id} not found.");
            }

         
            return new ComingAndGoingDto
            {
                Id = entity.Id,
                OnlineTime = entity.OnlineTime?.Select(o => new DateTimeDto
                {
                    TimeIn = o.TimeIn
                  
                }).ToList(),
                OflineTime = entity.OflineTime?.Select(o => new DateTimeDto
                {
                    TimeOut = o.TimeOut
                }).ToList(),
            };
        }
    }
}
