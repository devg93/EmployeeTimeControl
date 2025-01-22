
using System.Linq;
using System.Threading.Tasks;
using Modules.Break.Module.Core.Astractions.Irepository;
using Shared.Dto;
using Shared.Services.ModuleCommunication.Contracts;

namespace Break.Module.Core.ServicesCommunication
{
    public class SendDataToTimeInTimeOutModule:ISendDataToTimeInTimeOutModule
    {
        private readonly IbreakRepositoryQeury getservice;
        public SendDataToTimeInTimeOutModule(IbreakRepositoryQeury getservice)
        =>this.getservice=getservice;

        public async Task<BrakeTimeDto> GetByIdAsync(int id)
        {
            var brakeTime = await getservice.GetBreakByIdAsinc(id);
            return new BrakeTimeDto
            {
                Id = brakeTime.Id,
                StartTime = brakeTime.StartTime?.Select(tm=>new DateTimeWorkScheduleDto
                {
                    StartTime=tm.StartTime
                }).ToList(),
                EndTime = brakeTime.EndTime?.Select(tm => new DateTimeWorkScheduleDto
                {
                    EndTime = tm.EndTime
                }).ToList()
                
            };
        }
    }
}